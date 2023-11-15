using Kayord.IOT.Data;
using Microsoft.EntityFrameworkCore;
using Kayord.IOT.Features.Sensor;

namespace Kayord.IOT.Features.Sensor.GetAll;

public class Endpoint : EndpointWithoutRequest<List<SensorDto>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/sensor");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        SensorMapper m = new();
        var test = await _dbContext.Sensor.Select(s => m.MapSensorToSensorDto(s)).ToListAsync();
        await SendAsync(test);
    }
}