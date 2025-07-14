using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class VenueRoleEntityConfiguration : IEntityTypeConfiguration<VenueRoleEntity>
{
    public void Configure(EntityTypeBuilder<VenueRoleEntity> builder)
    {
        builder.HasIndex(vr => vr.Name)
               .IsUnique()
               .HasDatabaseName("ix_venue_roles_name");

        builder.HasKey(vr => vr.Id);

        builder.HasData(
            new VenueRoleEntity
            {
                Id = 1,
                Name = "venue-owner",
                Description = "Full control over venue settings, staff, and content"
            },
            new VenueRoleEntity
            {
                Id = 2,
                Name = "venue-manager",
                Description = "Can manage venue content, specials, and view reports"
            },
            new VenueRoleEntity
            {
                Id = 3,
                Name = "venue-staff",
                Description = "Can update specials and business hours"
            }
        );
    }
}