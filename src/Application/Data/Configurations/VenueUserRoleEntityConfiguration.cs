using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Data.Configurations;

public class VenueUserRoleEntityConfiguration : EntityBaseConfiguration<VenueUserRoleEntity>
{
    public override void Configure(EntityTypeBuilder<VenueUserRoleEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(vur => new { vur.UserId, vur.VenueId })
               .IsUnique()
               .HasDatabaseName("ix_venue_user_roles_user_venue");
        builder.HasIndex(vur => vur.VenueId)
               .HasDatabaseName("ix_venue_user_roles_venue_id");
        builder.HasIndex(vur => vur.IsActive)
               .HasDatabaseName("ix_venue_user_roles_is_active");
        builder.HasIndex(vur => new { vur.VenueId, vur.RoleId, vur.IsActive })
               .HasDatabaseName("ix_venue_user_roles_venue_role_active");

        builder.HasOne(vur => vur.User)
               .WithMany(u => u.VenueRoles)
               .HasForeignKey(vur => vur.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(vur => vur.Venue)
               .WithMany(v => v.VenueUsers)
               .HasForeignKey(vur => vur.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(vur => vur.Role)
               .WithMany(vr => vr.VenueUsers)
               .HasForeignKey(vur => vur.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(vur => vur.GrantedByUser)
               .WithMany()
               .HasForeignKey(vur => vur.GrantedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}