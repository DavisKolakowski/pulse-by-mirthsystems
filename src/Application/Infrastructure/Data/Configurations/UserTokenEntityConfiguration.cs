using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserTokenEntity>
{
    public void Configure(EntityTypeBuilder<UserTokenEntity> builder)
    {
        builder.ToTable("user_tokens");

        builder.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

        builder.Property(ut => ut.UserId)
            .HasColumnName("user_id");
        builder.Property(ut => ut.LoginProvider)
            .HasColumnName("login_provider")
            .HasMaxLength(128);
        builder.Property(ut => ut.Name)
            .HasColumnName("name")
            .HasMaxLength(128);
        builder.Property(ut => ut.Value)
            .HasColumnName("value");
    }
}