using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasIndex(u => u.NormalizedUserName)
            .HasDatabaseName("ix_users_normalized_user_name")
            .IsUnique();
        builder.HasIndex(u => u.NormalizedEmail)
            .HasDatabaseName("ix_users_normalized_email");
        builder.HasIndex(u => u.UserName)
            .HasDatabaseName("ix_users_user_name")
            .IsUnique();
        builder.HasIndex(u => u.Email)
            .HasDatabaseName("ix_users_email")
            .IsUnique();

        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName)
            .HasColumnName("normalized_user_name")
            .HasMaxLength(256);
        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail)
            .HasColumnName("normalized_email")
            .HasMaxLength(256);
        builder.Property(u => u.EmailConfirmed)
            .HasColumnName("email_confirmed");
        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash");
        builder.Property(u => u.SecurityStamp)
            .HasColumnName("security_stamp");
        builder.Property(u => u.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");
        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number");
        builder.Property(u => u.PhoneNumberConfirmed)
            .HasColumnName("phone_number_confirmed");
        builder.Property(u => u.TwoFactorEnabled)
            .HasColumnName("two_factor_enabled");
        builder.Property(u => u.LockoutEnd)
            .HasColumnName("lockout_end");
        builder.Property(u => u.LockoutEnabled)
            .HasColumnName("lockout_enabled");
        builder.Property(u => u.AccessFailedCount)
            .HasColumnName("access_failed_count");

        builder.HasMany(u => u.Claims)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(u => u.Logins)
               .WithOne(l => l.User)
               .HasForeignKey(l => l.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(u => u.Tokens)
               .WithOne(t => t.User)
               .HasForeignKey(t => t.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(u => u.UserRoles)
               .WithOne(ur => ur.User)
               .HasForeignKey(ur => ur.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(u => u.VenueRoles)
               .WithOne(vur => vur.User)
               .HasForeignKey(vur => vur.UserId)
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