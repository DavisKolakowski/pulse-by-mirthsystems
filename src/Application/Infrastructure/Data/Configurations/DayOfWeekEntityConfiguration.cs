using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for DayOfWeek entity
/// </summary>
public class DayOfWeekEntityConfiguration : IEntityTypeConfiguration<DayOfWeekEntity>
{
    public void Configure(EntityTypeBuilder<DayOfWeekEntity> builder)
    {
        builder.HasIndex(d => d.IsoNumber)
               .IsUnique();

        builder.HasKey(d => d.Id);

        builder.HasData(
            new DayOfWeekEntity
            {
                Id = 1,
                Name = "Sunday",
                ShortName = "SUN",
                IsoNumber = 7,
                IsWeekday = false,
                SortOrder = 1,            
            },
            new DayOfWeekEntity
            {
                Id = 2,
                Name = "Monday",
                ShortName = "MON",
                IsoNumber = 1,
                IsWeekday = true,
                SortOrder = 2,
            },
            new DayOfWeekEntity
            {
                Id = 3,
                Name = "Tuesday",
                ShortName = "TUE",
                IsoNumber = 2,
                IsWeekday = true,
                SortOrder = 3,              
            },
            new DayOfWeekEntity
            {
                Id = 4,
                Name = "Wednesday",
                ShortName = "WED",
                IsoNumber = 3,
                IsWeekday = true,
                SortOrder = 4,
            },
            new DayOfWeekEntity
            {
                Id = 5,
                Name = "Thursday",
                ShortName = "THU",
                IsoNumber = 4,
                IsWeekday = true,
                SortOrder = 5,
            },
            new DayOfWeekEntity
            {
                Id = 6,
                Name = "Friday",
                ShortName = "FRI",
                IsoNumber = 5,
                IsWeekday = true,
                SortOrder = 6,
            },
            new DayOfWeekEntity
            {
                Id = 7,
                Name = "Saturday",
                ShortName = "SAT",
                IsoNumber = 6,
                IsWeekday = false,
                SortOrder = 7,
            }
        );
    }
}
