using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class SpecialEntityConfiguration : EntityBaseConfiguration<SpecialEntity>
{
    public override void Configure(EntityTypeBuilder<SpecialEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(s => s.SpecialCategoryId)
               .HasDatabaseName("ix_specials_category_id");
        builder.HasIndex(s => s.IsActive)
               .HasDatabaseName("ix_specials_is_active");

        builder.HasOne(s => s.Category)
               .WithMany(sc => sc.Specials)
               .HasForeignKey(s => s.SpecialCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(s => s.SpecialMenu)
               .WithMany(sm => sm.Specials)
               .HasForeignKey(s => s.SpecialMenuId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.Property(s => s.AdditionalData)
               .HasColumnType("jsonb");
    }
}