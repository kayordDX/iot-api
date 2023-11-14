
using Kayord.IOT.Data;
using Kayord.IOT.Entities;

namespace Kayord.IOT.Features.Sensor.Create;

public class Endpoint : Endpoint<Request, Entities.Sensor>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/sensor");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Sensor entity = new Entities.Sensor()
        {
            Topic = req.Topic,
            Name = req.Name
        };
        await _dbContext.Sensor.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.Sensor.FindAsync(entity.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(result);
    }
}