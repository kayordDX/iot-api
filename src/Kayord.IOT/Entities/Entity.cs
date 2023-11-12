namespace Kayord.IOT.Entities;

public class Entity
{
    public DateTimeOffset Time { get; set; }
    public string Name { get; set; } = "";
    public decimal? Value { get; set; }
}