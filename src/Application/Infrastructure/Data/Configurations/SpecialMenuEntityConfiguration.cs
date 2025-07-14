using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class SpecialMenuEntityConfiguration : IEntityTypeConfiguration<SpecialMenuEntity>
{
    public void Configure(EntityTypeBuilder<SpecialMenuEntity> builder)
    {
        builder.HasIndex(sm => sm.VenueId)
               .HasDatabaseName("ix_special_menus_venue_id");
        builder.HasIndex(sm => new { sm.VenueId, sm.Name })
               .IsUnique()
               .HasDatabaseName("ix_special_menus_venue_name");

        builder.HasKey(sm => sm.Id);

        builder.HasOne(sm => sm.Venue)
               .WithMany(v => v.SpecialMenus)
               .HasForeignKey(sm => sm.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}