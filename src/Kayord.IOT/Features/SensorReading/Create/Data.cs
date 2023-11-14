using Kayord.IOT.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.IOT.Features.SensorReading.Create;

public static class Data
{
    public static async Task AddSensorReading(AppDbContext dbContext, string topic, decimal state)
    {
        var sensor = await dbContext.Sensor.Where(x => x.Topic.Equals(topic)).FirstOrDefaultAsync();
        if (sensor == null)
        {
            return;
        }

        await dbContext.SensorReading.AddAsync(new Entities.SensorReading()
        {
            SensorId = sensor.Id,
            Time = DateTime.Now,
            State = state
        });

        sensor.LastUpdated = DateTime.Now;
        sensor.State = state;

        await dbContext.SaveChangesAsync();
    }
}