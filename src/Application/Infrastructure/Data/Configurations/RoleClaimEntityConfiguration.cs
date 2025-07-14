using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class RoleClaimEntityConfiguration : IEntityTypeConfiguration<RoleClaimEntity>
{
    public void Configure(EntityTypeBuilder<RoleClaimEntity> builder)
    {
        builder.ToTable("role_claims");

        builder.HasKey(rc => rc.Id);

        builder.Property(rc => rc.Id)
            .HasColumnName("id");
        builder.Property(rc => rc.RoleId)
            .HasColumnName("role_id");
        builder.Property(rc => rc.ClaimType)
            .HasColumnName("claim_type");
        builder.Property(rc => rc.ClaimValue)
            .HasColumnName("claim_value");
    }
}