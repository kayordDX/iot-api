using Riok.Mapperly.Abstractions;

namespace Kayord.IOT.Features.Sensor;

[Mapper]
public static partial class SensorMapper
{
    public static partial IQueryable<SensorDto> ProjectToDto(this IQueryable<Entities.Sensor> q);
    private static int TimeSpanToHours(TimeSpan t) => t.Hours;
}