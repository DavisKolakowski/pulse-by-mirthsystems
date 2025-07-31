using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class VenueBusinessHoursEntityConfiguration : EntityBaseConfiguration<VenueBusinessHoursEntity>
{
    public override void Configure(EntityTypeBuilder<VenueBusinessHoursEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(bh => new { bh.VenueId, bh.DayOfWeekId })
               .IsUnique()
               .HasDatabaseName("ix_venue_business_hours_day");

        builder.HasOne(bh => bh.Venue)
               .WithMany(v => v.BusinessHours)
               .HasForeignKey(bh => bh.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(bh => bh.DayOfWeek)
               .WithMany(dow => dow.BusinessHours)
               .HasForeignKey(bh => bh.DayOfWeekId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}