using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Data.Configurations;

public class VenueEntityConfiguration : EntityBaseConfiguration<VenueEntity>
{
    public override void Configure(EntityTypeBuilder<VenueEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(v => v.Name)
               .HasDatabaseName("ix_venues_name");
        builder.HasIndex(v => v.Location)
               .HasMethod("GIST")
               .HasDatabaseName("ix_venues_location");
        builder.HasIndex(v => v.PrimaryCategoryId)
               .HasDatabaseName("ix_venues_primary_category_id");
        builder.HasIndex(e => e.SecondaryCategoryId)
               .HasDatabaseName("ix_venues_secondary_category_id");
        builder.HasIndex(e => e.TimeZoneId)
               .HasDatabaseName("ix_venues_time_zone_id");
        builder.HasIndex(v => v.IsActive)
               .HasDatabaseName("ix_venues_is_active");
        builder.HasIndex(v => new { v.IsActive, v.PrimaryCategoryId })
               .HasDatabaseName("ix_venues_active_category");

        builder.HasOne(v => v.PrimaryCategory)
               .WithMany(vc => vc.PrimaryVenues)
               .HasForeignKey(v => v.PrimaryCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.SecondaryCategory)
               .WithMany(c => c.SecondaryVenues)
               .HasForeignKey(e => e.SecondaryCategoryId)
               .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(v => v.BusinessHours)
               .WithOne(bh => bh.Venue)
               .HasForeignKey(bh => bh.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(v => v.SpecialMenus)
               .WithOne(sm => sm.Venue)
               .HasForeignKey(sm => sm.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(v => v.VenueUsers)
               .WithOne(vur => vur.Venue)
               .HasForeignKey(vur => vur.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}