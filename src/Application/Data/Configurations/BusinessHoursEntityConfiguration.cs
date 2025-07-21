using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Data.Configurations;

public class BusinessHoursEntityConfiguration : EntityBaseConfiguration<BusinessHoursEntity>
{
    public override void Configure(EntityTypeBuilder<BusinessHoursEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(bh => new { bh.VenueId, bh.DayOfWeekId })
               .IsUnique()
               .HasDatabaseName("ix_business_hours_venue_day");

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