using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class VenueRoleEntityConfiguration : EntityBaseConfiguration<VenueRoleEntity>
{
    public override void Configure(EntityTypeBuilder<VenueRoleEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(vr => vr.Name)
               .IsUnique()
               .HasDatabaseName("ix_venue_roles_name");
    }
}