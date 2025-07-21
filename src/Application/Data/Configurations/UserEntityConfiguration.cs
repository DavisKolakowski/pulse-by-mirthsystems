using Application.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Data.Configurations;

public class UserEntityConfiguration : EntityBaseConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(u => u.NameIdentifier)
               .IsUnique()
               .HasDatabaseName("ix_users_sub");
        builder.HasIndex(u => u.EmailAddress)
               .IsUnique()
               .HasDatabaseName("ix_users_email_address");
        builder.HasIndex(u => u.IsActive)
               .HasDatabaseName("ix_users_is_active");

        builder.HasMany(u => u.VenueRoles)
               .WithOne(uvp => uvp.User)
               .HasForeignKey(uvp => uvp.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(u => u.SentInvitations)
               .WithOne(vi => vi.InvitedByUser)
               .HasForeignKey(vi => vi.InvitedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.ReceivedInvitations)
               .WithOne(vi => vi.AcceptedByUser)
               .HasForeignKey(vi => vi.AcceptedByUserId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
