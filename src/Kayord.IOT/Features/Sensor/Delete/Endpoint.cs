using Kayord.IOT.Data;

namespace Kayord.IOT.Features.Sensor.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/sensor");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Sensor.FindAsync(req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        _dbContext.Sensor.Remove(entity);
        await _dbContext.SaveChangesAsync();

        await SendNoContentAsync();
    }
}