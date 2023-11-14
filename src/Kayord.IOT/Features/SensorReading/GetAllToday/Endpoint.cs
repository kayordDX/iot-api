using Kayord.IOT.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.IOT.Features.SensorReading.GetAllToday;

public class Endpoint : EndpointWithoutRequest<List<Entities.SensorReading>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/sensorReading");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var today = DateTime.Today.AddDays(-1);
        var results = await _dbContext.SensorReading.Where(x => x.Time >= today).ToListAsync();
        await SendAsync(results);
    }
}