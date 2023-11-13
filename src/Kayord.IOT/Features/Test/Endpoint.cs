namespace Kayord.POS.Features.Test;

public class Endpoint : Endpoint<object, object>
{
    private readonly ILogger<Endpoint> _logger;

    public Endpoint(ILogger<Endpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(object req, CancellationToken ct)
    {
        _logger.LogInformation("Test Response {$Data}", req);
        await SendAsync(req);
    }
}