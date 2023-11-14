using Kayord.IOT.Data;
using Microsoft.EntityFrameworkCore;

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
        var results = await _dbContext.Sensor.ProjectToDto().ToListAsync();
        await SendAsync(results);
    }
}