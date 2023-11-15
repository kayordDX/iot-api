using Riok.Mapperly.Abstractions;

namespace Kayord.IOT.Features.Sensor;

[Mapper]
public static partial class SensorMapperStatic
{
    public static partial IQueryable<SensorDto> ProjectToDto(this IQueryable<Entities.Sensor> q);
}