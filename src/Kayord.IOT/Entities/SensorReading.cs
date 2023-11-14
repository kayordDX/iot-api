namespace Kayord.IOT.Entities;

public class SensorReading
{
    public int SensorId { get; set; }
    public DateTime Time { get; set; }
    public decimal? State { get; set; }
}