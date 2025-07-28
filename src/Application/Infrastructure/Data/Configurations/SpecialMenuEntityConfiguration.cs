using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class SpecialMenuEntityConfiguration : EntityBaseConfiguration<SpecialMenuEntity>
{
    public override void Configure(EntityTypeBuilder<SpecialMenuEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(sm => sm.VenueId)
               .HasDatabaseName("ix_special_menus_venue_id");
        builder.HasIndex(sm => new { sm.VenueId, sm.Name })
               .IsUnique()
               .HasDatabaseName("ix_special_menus_venue_name");

        builder.HasOne(sm => sm.Venue)
               .WithMany(v => v.SpecialMenus)
               .HasForeignKey(sm => sm.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}