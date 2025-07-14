using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasIndex(r => r.NormalizedName)
            .HasDatabaseName("ix_roles_normalized_name")
            .IsUnique();

        builder.ToTable("roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .HasColumnName("name")
            .HasMaxLength(256);
        builder.Property(r => r.NormalizedName)
            .HasColumnName("normalized_name")
            .HasMaxLength(256);
        builder.Property(r => r.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");

        builder.HasMany(r => r.UserRoles)
               .WithOne(ur => ur.Role)
               .HasForeignKey(ur => ur.RoleId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(r => r.RoleClaims)
               .WithOne(rc => rc.Role)
               .HasForeignKey(rc => rc.RoleId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}