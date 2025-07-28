using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class VenueCategoryEntityConfiguration : EntityBaseConfiguration<VenueCategoryEntity>
{
    public override void Configure(EntityTypeBuilder<VenueCategoryEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(vc => vc.Name)
               .IsUnique()
               .HasDatabaseName("ix_venue_categories_name");

        builder.HasMany(vc => vc.PrimaryVenues)
               .WithOne(v => v.PrimaryCategory)
               .HasForeignKey(v => v.PrimaryCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(vc => vc.SecondaryVenues)
               .WithOne(v => v.SecondaryCategory)
               .HasForeignKey(v => v.SecondaryCategoryId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}