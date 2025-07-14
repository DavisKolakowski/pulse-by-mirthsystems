using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class UserClaimEntityConfiguration : IEntityTypeConfiguration<UserClaimEntity>
{
    public void Configure(EntityTypeBuilder<UserClaimEntity> builder)
    {
        builder.ToTable("user_claims");

        builder.HasKey(uc => uc.Id);

        builder.Property(uc => uc.Id)
            .HasColumnName("id");
        builder.Property(uc => uc.UserId)
            .HasColumnName("user_id");
        builder.Property(uc => uc.ClaimType)
            .HasColumnName("claim_type");
        builder.Property(uc => uc.ClaimValue)
            .HasColumnName("claim_value");
    }
}