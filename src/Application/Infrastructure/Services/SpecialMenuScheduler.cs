using System.Threading;

using Application.Contracts.Services;
using Application.Entities;
using Application.Enums;
using Application.Infrastructure.Data;
using Application.Models.Requests;

using Cronos;

using Hangfire;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

using NodaTime;

namespace Application.Infrastructure.Services;

public class SpecialMenuScheduler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IClock _clock;
    private readonly IRecurringJobManager _jobManager;
    private readonly ILogger<SpecialMenuScheduler> _logger;
    private static string GetJobId(Guid scheduleId)
    {
        return $"special-menu-schedule-{scheduleId}";
    }

    public SpecialMenuScheduler(
        ApplicationDbContext dbContext,
        IClock clock,
        IRecurringJobManager hangfireManager,
        ILogger<SpecialMenuScheduler> logger)
    {
        _dbContext = dbContext;
        _clock = clock;
        _jobManager = hangfireManager;
        _logger = logger;
    }

    public async Task AddOrUpdate(SpecialMenuScheduleEntity schedule)
    {
        UpdateStatus(schedule);

        var existingSchedule = await _dbContext.SpecialMenuSchedules
            .FirstOrDefaultAsync(s => s.Id == schedule.Id);
        if (existingSchedule == null)
        {
            _dbContext.SpecialMenuSchedules.Add(schedule);
            _logger.LogInformation("Added new schedule with ID {ScheduleId}.", schedule.Id);
        }
        else
        {
            _dbContext.Entry(existingSchedule).CurrentValues.SetValues(schedule);
            _logger.LogInformation("Updated schedule with ID {ScheduleId}.", schedule.Id);
        }
        await _dbContext.SaveChangesAsync();
        Schedule(schedule);
    }

    public async Task Remove(Guid scheduleId)
    {
        var schedule = await _dbContext.SpecialMenuSchedules
            .FirstOrDefaultAsync(s => s.Id == scheduleId);
        if (schedule == null)
        {
            _logger.LogWarning("Schedule with ID {ScheduleId} does not exist.", scheduleId);
            return;
        }
        _dbContext.SpecialMenuSchedules.Remove(schedule);
        await _dbContext.SaveChangesAsync();
        Delete(scheduleId);
    }

    private void Schedule(SpecialMenuScheduleEntity schedule)
    {
        if (!schedule.IsActive ||
            schedule.Status == SpecialMenuScheduleStatus.Expired ||
            schedule.Status == SpecialMenuScheduleStatus.Disabled)
        {
            Delete(schedule.Id);
            return;
        }

        var cron = schedule.CronPattern.ToString();
        var jobId = GetJobId(schedule.Id);

        _jobManager.AddOrUpdate(
            jobId,
            () => Start(schedule.Id),
            cron,
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneId(schedule))
            }
        );
    }

    private async Task Start(Guid scheduleId)
    {
        var schedule = await _dbContext.SpecialMenuSchedules.FindAsync(scheduleId);
        if (schedule == null) return;

        schedule.Status = SpecialMenuScheduleStatus.Running;
        await _dbContext.SaveChangesAsync();

        var duration = CalculateScheduleDuration(schedule);
        BackgroundJob.Schedule(
            () => Stop(scheduleId),
            duration.ToTimeSpan()
        );
    }

    private async Task Stop(Guid scheduleId)
    {
        var schedule = await _dbContext.SpecialMenuSchedules.FindAsync(scheduleId);
        if (schedule == null)
        {
            return;
        }

        UpdateStatus(schedule);
        await _dbContext.SaveChangesAsync();
    }

    private void UpdateStatus(SpecialMenuScheduleEntity schedule)
    {
        string timeZoneId = GetTimeZoneId(schedule);

        var timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZoneId);

        if (timeZone == null)
        {
            _logger.LogError("Invalid venue time zone: {TimeZoneId} for SpecialMenuId: {SpecialMenuId}", timeZoneId, schedule.SpecialMenuId);
            schedule.Status = SpecialMenuScheduleStatus.Disabled;
            return;
        }

        if (!schedule.IsActive)
        {
            schedule.Status = SpecialMenuScheduleStatus.Disabled;
            return;
        }

        var nowInVenueZone = _clock.GetCurrentInstant().InZone(timeZone);
        var today = nowInVenueZone.Date;

        if (schedule.ExpirationDate.HasValue && today > schedule.ExpirationDate.Value)
        {
            schedule.Status = SpecialMenuScheduleStatus.Expired;
            return;
        }

        if (today >= schedule.StartDate && (!schedule.ExpirationDate.HasValue || today <= schedule.ExpirationDate.Value))
        {
            schedule.Status = SpecialMenuScheduleStatus.Scheduled;
            return;
        }

        schedule.Status = SpecialMenuScheduleStatus.Expired;
    }

    private string GetTimeZoneId(SpecialMenuScheduleEntity schedule)
    {
        return _dbContext.SpecialMenus
            .Where(m => m.Id == schedule.SpecialMenuId)
            .Select(m => m.Venue.TimeZoneId)
            .Single();
    }

    private Duration CalculateScheduleDuration(SpecialMenuScheduleEntity schedule)
    {
        var start = schedule.StartTime;
        var end = schedule.EndTime;

        if (end < start)
        {
            end = end.PlusHours(24);
        }
        var period = end - start;
        return period.ToDuration();
    }

    private void Delete(Guid scheduleId)
    {
        var jobId = GetJobId(scheduleId);
        _jobManager.RemoveIfExists(jobId);
    }
}