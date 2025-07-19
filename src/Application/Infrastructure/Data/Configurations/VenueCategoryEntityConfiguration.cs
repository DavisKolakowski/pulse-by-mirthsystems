using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class VenueCategoryEntityConfiguration : EntityBaseConfiguration<VenueCategoryEntity>
{
    public override void Configure(EntityTypeBuilder<VenueCategoryEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(vc => vc.Name)
               .IsUnique()
               .HasDatabaseName("ix_venue_categories_name");

        builder.HasMany(vc => vc.Venues)
               .WithOne(v => v.Category)
               .HasForeignKey(v => v.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}