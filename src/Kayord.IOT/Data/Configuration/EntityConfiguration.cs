using Kayord.IOT.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.IOT.Data.Configuration;

public class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.HasNoKey();
        builder.Property(t => t.Name).HasMaxLength(250).IsRequired();
        builder.Property(t => t.Value).IsRequired();
    }
}