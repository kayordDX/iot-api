using Kayord.IOT.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.IOT.Data.Configuration;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
        builder.Property(t => t.Topic).HasMaxLength(250).IsRequired();
    }
}