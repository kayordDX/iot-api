namespace Kayord.IOT.Features.Sensor;

public class SensorDto
{
    public int Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string? Name { get; set; }
    public decimal? State { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string LastUpdatedString { get; set; } = string.Empty;
}