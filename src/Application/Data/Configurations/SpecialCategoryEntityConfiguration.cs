using Application.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Data.Configurations;

public class SpecialCategoryEntityConfiguration : EntityBaseConfiguration<SpecialCategoryEntity>
{
    public override void Configure(EntityTypeBuilder<SpecialCategoryEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(sc => sc.Name)
               .IsUnique();
    }
}
