using Kayord.IOT.Data;
using Kayord.IOT.Hubs;
using Microsoft.AspNetCore.SignalR;
using MQTTnet;
using MQTTnet.Client;

namespace Kayord.IOT.Services;

public class Mqtt : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<Mqtt> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHubContext<ChatHub> _hub;

    public Mqtt(IConfiguration configuration, ILogger<Mqtt> logger, IServiceScopeFactory serviceScopeFactory, IHubContext<ChatHub> hub)
    {
        _configuration = configuration;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _hub = hub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

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
                    _logger.LogInformation("The MQTT client is connected.");
                    var mqttSubscribeOptions = factory.CreateSubscribeOptionsBuilder()
                        .WithTopicFilter(
                            f =>
                            {
                                f.WithTopic("+/sensor/#");
                            })
                        .Build();

                    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                    mqttClient.ApplicationMessageReceivedAsync += async e =>
                    {
                        await _hub.Clients.All.SendAsync("ReceiveMessage", e.ApplicationMessage.Topic);
                        _logger.LogInformation(e.ApplicationMessage.Topic);
                        // e.ApplicationMessage.PayloadSegment
                        string result = e.ApplicationMessage.ConvertPayloadToString();
                        _logger.LogInformation(result);
                        decimal state = decimal.Parse(result);
                        await Features.SensorReading.Create.Data.AddSensorReading(dbContext, e.ApplicationMessage.Topic, state);
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error massive problem", ex);
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