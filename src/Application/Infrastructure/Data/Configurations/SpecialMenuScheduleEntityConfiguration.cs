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
    }
}