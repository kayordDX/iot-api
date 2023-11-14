using FluentValidation;

namespace Kayord.IOT.Features.Sensor.Edit;

public class Request
{
    public int Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string? Name { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(v => v.Topic).NotEmpty().WithMessage("Topic is required");
    }
}