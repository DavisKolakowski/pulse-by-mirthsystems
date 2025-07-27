using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Entities;

namespace Application.Data.Configurations;

public class SpecialMenuScheduleEntityConfiguration : EntityBaseConfiguration<SpecialMenuScheduleEntity>
{
    public override void Configure(EntityTypeBuilder<SpecialMenuScheduleEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(sms => sms.SpecialMenuId)
               .HasDatabaseName("ix_special_menu_schedules_menu_id");
        builder.HasIndex(sms => sms.IsActive)
               .HasDatabaseName("ix_special_menu_schedules_is_active");
        builder.HasIndex(sms => new { sms.SpecialMenuId, sms.IsActive })
               .HasDatabaseName("ix_special_menu_schedules_menu_active");

        builder.HasOne(sms => sms.SpecialMenu)
               .WithMany(sm => sm.Schedules)
               .HasForeignKey(sms => sms.SpecialMenuId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(sms => sms.RecurrenceSettings, rs =>
        {
            rs.ToJson("recurrence_settings");

            rs.Property(p => p.Name)
              .HasJsonPropertyName("name");

            rs.Property(p => p.Description)
              .HasJsonPropertyName("description");

            rs.OwnsOne(p => p.CronPattern, cp =>
            {
                cp.Property(c => c.Seconds)
                  .HasJsonPropertyName("seconds")
                  .HasMaxLength(50);
                cp.Property(c => c.Minutes)
                  .HasJsonPropertyName("minutes")
                  .HasMaxLength(50);
                cp.Property(c => c.Hours)
                  .HasJsonPropertyName("hours")
                  .HasMaxLength(50);
                cp.Property(c => c.DayOfMonth)
                  .HasJsonPropertyName("day_of_month")
                  .HasMaxLength(50);
                cp.Property(c => c.Month)
                  .HasJsonPropertyName("month")
                  .HasMaxLength(50);
                cp.Property(c => c.DayOfWeek)
                  .HasJsonPropertyName("day_of_week")
                  .HasMaxLength(50);
                cp.Property(c => c.Year)
                  .HasJsonPropertyName("year")
                  .HasMaxLength(50);
            });
        });
    }
}