using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasIndex(u => u.Sub)
               .IsUnique()
               .HasDatabaseName("ix_users_sub");
        builder.HasIndex(u => u.Email)
               .IsUnique()
               .HasDatabaseName("ix_users_email");
        builder.HasIndex(u => u.IsActive)
               .HasDatabaseName("ix_users_is_active");

        builder.HasKey(u => u.Id);

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
