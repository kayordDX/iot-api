namespace Kayord.IOT.Entities;

public class Sensor
{
    public int Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string? Name { get; set; }
    public decimal? State { get; set; }
    public DateTime? LastUpdated { get; set; }
}