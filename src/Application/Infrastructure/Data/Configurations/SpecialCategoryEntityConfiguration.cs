using Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Data.Configurations;

public class SpecialCategoryEntityConfiguration : IEntityTypeConfiguration<SpecialCategoryEntity>
{
    public void Configure(EntityTypeBuilder<SpecialCategoryEntity> builder)
    {
        builder.HasIndex(sc => sc.Name)
               .IsUnique();

        builder.HasKey(sc => sc.Id);

        builder.HasData(
            new SpecialCategoryEntity
            {
                Id = 1,
                Name = "Food",
                Description = "Food specials, appetizers, and meal deals",
                Icon = "🍔",
                SortOrder = 1,
            },
            new SpecialCategoryEntity
            {
                Id = 2,
                Name = "Drink",
                Description = "Drink specials, happy hours, and beverage promotions",
                Icon = "🍺",
                SortOrder = 2,
            },
            new SpecialCategoryEntity
            {
                Id = 3,
                Name = "Entertainment",
                Description = "Live music, DJs, trivia, karaoke, and other events",
                Icon = "🎵",
                SortOrder = 3,
            }
        );
    }
}
