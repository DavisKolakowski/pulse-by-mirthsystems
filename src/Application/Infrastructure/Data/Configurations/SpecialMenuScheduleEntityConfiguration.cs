using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class SpecialMenuScheduleEntityConfiguration : IEntityTypeConfiguration<SpecialMenuScheduleEntity>
{
    public void Configure(EntityTypeBuilder<SpecialMenuScheduleEntity> builder)
    {
        builder.HasIndex(sms => sms.SpecialMenuId)
               .HasDatabaseName("ix_special_menu_schedules_menu_id");
        builder.HasIndex(sms => sms.IsActive)
               .HasDatabaseName("ix_special_menu_schedules_is_active");
        builder.HasIndex(sms => new { sms.SpecialMenuId, sms.IsActive })
               .HasDatabaseName("ix_special_menu_schedules_menu_active");

        builder.HasKey(sms => sms.Id);

        builder.HasOne(sms => sms.SpecialMenu)
               .WithMany(sm => sm.Schedules)
               .HasForeignKey(sms => sms.SpecialMenuId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(sms => sms.RecurrencePattern, rp =>
        {
            rp.ToJson("recurrence_pattern");

            rp.Property(p => p.Name)
              .HasJsonPropertyName("name");
            rp.Property(p => p.Seconds)
              .HasJsonPropertyName("seconds")
              .HasMaxLength(50);
            rp.Property(p => p.Minutes)
              .HasJsonPropertyName("minutes")
              .HasMaxLength(50);
            rp.Property(p => p.Hours)
              .HasJsonPropertyName("hours")
              .HasMaxLength(50);
            rp.Property(p => p.DayOfMonth)
              .HasJsonPropertyName("day_of_month")
              .HasMaxLength(50);
            rp.Property(p => p.Month)
              .HasJsonPropertyName("month")
              .HasMaxLength(50);
            rp.Property(p => p.DayOfWeek)
              .HasJsonPropertyName("day_of_week")
              .HasMaxLength(50);
            rp.Property(p => p.Year)
              .HasJsonPropertyName("year")
              .HasMaxLength(50);
        });
    }
}