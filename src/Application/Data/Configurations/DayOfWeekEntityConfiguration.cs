using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Data.Configurations;

/// <summary>
/// Entity configuration for DayOfWeek entity
/// </summary>
public class DayOfWeekEntityConfiguration : EntityBaseConfiguration<DayOfWeekEntity>
{
    public override void Configure(EntityTypeBuilder<DayOfWeekEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(d => d.IsoNumber)
               .IsUnique();
    }
}
