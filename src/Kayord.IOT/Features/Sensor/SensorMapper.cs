using Humanizer;
using Riok.Mapperly.Abstractions;

namespace Kayord.IOT.Features.Sensor;

[Mapper]
public partial class SensorMapper
{
    public SensorDto MapSensorToSensorDto(Entities.Sensor sensor)
    {
        var dto = SensorToSensorDto(sensor);
        dto.LastUpdatedString = dto.LastUpdated.Humanize();
        return dto;
    }

    private partial SensorDto SensorToSensorDto(Entities.Sensor sensor);
}