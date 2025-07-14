using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class UserLoginEntityConfiguration : IEntityTypeConfiguration<UserLoginEntity>
{
    public void Configure(EntityTypeBuilder<UserLoginEntity> builder)
    {
        builder.ToTable("user_logins");

        builder.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

        builder.Property(ul => ul.LoginProvider)
            .HasColumnName("login_provider")
            .HasMaxLength(128);
        builder.Property(ul => ul.ProviderKey)
            .HasColumnName("provider_key")
            .HasMaxLength(128);
        builder.Property(ul => ul.ProviderDisplayName)
            .HasColumnName("provider_display_name");
        builder.Property(ul => ul.UserId)
            .HasColumnName("user_id");
    }
}