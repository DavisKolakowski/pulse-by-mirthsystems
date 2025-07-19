using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

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
        builder.HasIndex(v => v.CategoryId)
               .HasDatabaseName("ix_venues_category_id");
        builder.HasIndex(v => v.IsActive)
               .HasDatabaseName("ix_venues_is_active");
        builder.HasIndex(v => new { v.IsActive, v.CategoryId })
               .HasDatabaseName("ix_venues_active_category");

        builder.HasOne(v => v.Category)
               .WithMany(vc => vc.Venues)
               .HasForeignKey(v => v.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(v => v.BusinessHours)
               .WithOne(bh => bh.Venue)
               .HasForeignKey(bh => bh.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(v => v.Specials)
               .WithOne(s => s.Venue)
               .HasForeignKey(s => s.VenueId)
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