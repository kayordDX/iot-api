using Humanizer;
using MQTTnet;
using MQTTnet.Client;

namespace Kayord.IOT.Services;

public class Mqtt : BackgroundService
{
    private readonly IConfiguration _configuration;

    public Mqtt(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new MqttFactory();
        using var mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTlsOptions(new MqttClientTlsOptions()
            {
                UseTls = false
            })
            .WithTcpServer(_configuration["MQTT:Server"])
            .WithCredentials(_configuration["MQTT:User"], _configuration["MQTT:Password"])
            .WithCleanSession()
            .Build();

        // User proper cancellation and no while(true).
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
                if (!await mqttClient.TryPingAsync())
                {
                    await mqttClient.ConnectAsync(options, CancellationToken.None);
                    // Subscribe to topics when session is clean etc.
                    Console.WriteLine("The MQTT client is connected.");

                    var mqttSubscribeOptions = factory.CreateSubscribeOptionsBuilder()
                        .WithTopicFilter(
                            f =>
                            {
                                f.WithTopic("+/sensor/#");
                            })
                        .Build();

                    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                    mqttClient.ApplicationMessageReceivedAsync += e =>
                    {
                        Console.WriteLine(e.ApplicationMessage.Topic);
                        // e.ApplicationMessage.PayloadSegment
                        string result = e.ApplicationMessage.ConvertPayloadToString();
                        Console.WriteLine(result);
                        return Task.CompletedTask;
                    };
                }
            }
            catch
            {
                Console.WriteLine("Error massive problem");
                // Handle the exception properly (logging etc.).
            }
            finally
            {
                // Check the connection state every 5 seconds and perform a reconnect if required.
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        await mqttClient.DisconnectAsync(new MqttClientDisconnectOptionsBuilder().WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection).Build());
    }
}