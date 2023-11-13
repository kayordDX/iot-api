global using FastEndpoints;

using KayordKit.Extensions.Api;
using Kayord.IOT.Data;
using Microsoft.EntityFrameworkCore;
using KayordKit.Extensions.Host;
using KayordKit.Extensions.Health;
using Kayord.IOT.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Configuration);
builder.Services.ConfigureApi();

builder.Services.AddHealthChecks()
            .AddProcessAllocatedMemoryHealthCheck(150);
builder.Services.AddHostedService<Mqtt>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
    // options.EnableSensitiveDataLogging();
});

var app = builder.Build();
app.UseApi();
app.UseHealth();
app.Run();


// Console.WriteLine("Starting up");
// var db = new AppDbContext();

// var sql = """
// SELECT * FROM "Entity"
// WHERE "Time" > now() - INTERVAL '1 week'
// """;

// string insertSql = $"INSERT INTO \"Entity\"(\"Time\", \"Name\", \"Value\") VALUES('{DateTime.Now}', 'Test', 29)";
// var what = db.Database.ExecuteSqlRaw(insertSql);

// var entities = db.Entity.FromSqlRaw(sql).Count();
// Console.WriteLine($"{entities} entities in the last week");

// Console.WriteLine("Done");

// Console.WriteLine($"{DateTime.Now.ToLocalTime()}");

// var factory = new MqttFactory();
// using var mqttClient = factory.CreateMqttClient();

// var options = new MqttClientOptionsBuilder()
//     .WithTlsOptions(new MqttClientTlsOptions()
//     {
//         UseTls = false
//     })
//     .WithTcpServer("server")
//     .WithCredentials("mqtt", "nhX6hyZY65aLDMgF")
//     .WithCleanSession()
//     .Build();


// _ = Task.Run(
//     async () =>
//     {
//         // User proper cancellation and no while(true).
//         while (true)
//         {
//             try
//             {
//                 // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
//                 if (!await mqttClient.TryPingAsync())
//                 {
//                     await mqttClient.ConnectAsync(options, CancellationToken.None);
//                     // Subscribe to topics when session is clean etc.
//                     Console.WriteLine("The MQTT client is connected.");
//                 }
//             }
//             catch
//             {
//                 Console.WriteLine("Error massive problem");
//                 // Handle the exception properly (logging etc.).
//             }
//             finally
//             {
//                 // Check the connection state every 5 seconds and perform a reconnect if required.
//                 await Task.Delay(TimeSpan.FromSeconds(5));
//             }
//         }
//     });    

// mqttClient.ApplicationMessageReceivedAsync += e =>
// {
//     string result = e.ApplicationMessage.ConvertPayloadToString();
//     Console.WriteLine(result);
//     return Task.CompletedTask;
// };

// await mqttClient.ConnectAsync(options, CancellationToken.None);

// var mqttSubscribeOptions = factory.CreateSubscribeOptionsBuilder()
//     .WithTopicFilter(
//         f =>
//         {
//             f.WithTopic("#");
//         })
//     .Build();

// await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

// Console.WriteLine("MQTT client subscribed to topic.");

// Console.WriteLine("Press enter to exit.");
// Console.ReadLine();

