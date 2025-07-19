using Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Data.Configurations;

public class SpecialCategoryEntityConfiguration : EntityBaseConfiguration<SpecialCategoryEntity>
{
    public override void Configure(EntityTypeBuilder<SpecialCategoryEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(sc => sc.Name)
               .IsUnique();
    }
}
