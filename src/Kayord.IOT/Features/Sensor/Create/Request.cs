using FluentValidation;

namespace Kayord.IOT.Features.Sensor.Create;

public class Request
{
    public string Topic { get; set; } = string.Empty;
    public string? Name { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Topic).NotEmpty().WithMessage("Topic is required");
    }
}