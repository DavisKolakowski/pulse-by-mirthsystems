using Application.Data.Queries;
using Application.Entities;
using Application.Enums;

using Microsoft.EntityFrameworkCore;

using NodaTime;

namespace Application.Data.Extensions;

public static class SpecialMenuScheduleQueryExtensions
{
    public static IQueryable<SpecialMenuScheduleEntity> Search(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Action<SpecialMenuScheduleSearchQuery> configureQuery)
    {
        var searchQuery = new SpecialMenuScheduleSearchQuery();
        configureQuery(searchQuery);

        query = query
            .Include(s => s.SpecialMenu)
                .ThenInclude(sm => sm.Venue)
                    .ThenInclude(v => v.PrimaryCategory)
            .Include(s => s.SpecialMenu)
                .ThenInclude(sm => sm.Venue)
                    .ThenInclude(v => v.SecondaryCategory)
            .Include(s => s.SpecialMenu)
                .ThenInclude(sm => sm.Venue)
                    .ThenInclude(v => v.BusinessHours)
                        .ThenInclude(bh => bh.DayOfWeek)
            .Include(s => s.SpecialMenu)
                .ThenInclude(sm => sm.Specials)
                    .ThenInclude(sp => sp.Category);

        query = query.Where(s => s.SpecialMenu.Venue.Location.Within(searchQuery.SearchArea));

        query = query.Where(s => s.IsActive && s.SpecialMenu.Venue.IsActive);

        if (!string.IsNullOrWhiteSpace(searchQuery.SearchTerm))
        {
            var searchTerm = searchQuery.SearchTerm.ToLower();
            query = query.Where(s =>
                s.SpecialMenu.Name.ToLower().Contains(searchTerm) ||
                (s.SpecialMenu.Description != null && s.SpecialMenu.Description.ToLower().Contains(searchTerm)) ||
                s.SpecialMenu.Venue.Name.ToLower().Contains(searchTerm) ||
                s.SpecialMenu.Specials.Any(sp => sp.Description.ToLower().Contains(searchTerm)));
        }

        if (searchQuery.SpecialCategoryIds.Any())
        {
            query = query.Where(s =>
                s.SpecialMenu.Specials.Any(sp =>
                    searchQuery.SpecialCategoryIds.Contains(sp.SpecialCategoryId) && sp.IsActive));
        }

        if (searchQuery.VenueCategoryIds.Any())
        {
            query = query.Where(s =>
                searchQuery.VenueCategoryIds.Contains(s.SpecialMenu.Venue.PrimaryCategoryId) ||
                (s.SpecialMenu.Venue.SecondaryCategoryId.HasValue &&
                 searchQuery.VenueCategoryIds.Contains(s.SpecialMenu.Venue.SecondaryCategoryId.Value)));
        }

        var centerPoint = searchQuery.SearchArea.Centroid;
        query = searchQuery.SortBy switch
        {
            SearchSpecialMenusSortByFilterSelections.Distance => searchQuery.SortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(s => s.SpecialMenu.Venue.Location.Distance(centerPoint))
                : query.OrderByDescending(s => s.SpecialMenu.Venue.Location.Distance(centerPoint)),
            SearchSpecialMenusSortByFilterSelections.ItemCount => searchQuery.SortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(s => s.SpecialMenu.Specials.Count(sp => sp.IsActive))
                : query.OrderByDescending(s => s.SpecialMenu.Specials.Count(sp => sp.IsActive)),
            _ => query.OrderBy(s => s.SpecialMenu.Venue.Location.Distance(centerPoint))
        };

        return query;
    }

    public static IQueryable<SpecialMenuScheduleEntity> WhereRunningOn(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Action<RunningSpecialMenuSchedulesQuery> configureQuery)
    {
        var searchQuery = new RunningSpecialMenuSchedulesQuery();
        configureQuery(searchQuery);

        var date = searchQuery.SearchDateTime.Date;
        var time = searchQuery.SearchDateTime.TimeOfDay;

        // Filter by date range
        query = query.Where(s =>
            s.StartDate <= date &&
            (!s.ExpirationDate.HasValue || s.ExpirationDate.Value >= date));

        // Filter by time range
        query = query.Where(s =>
            s.StartTime <= time &&
            s.EndTime >= time);

        // Get the cron values for the search date/time
        var dayOfWeek = ((int)date.DayOfWeek == 0) ? 7 : (int)date.DayOfWeek;
        var dayOfMonth = date.Day;
        var month = date.Month;
        var year = date.Year;

        // Filter by recurrence pattern using the JSONB fields
        query = query.Where(s =>
            // One-time events
            (!s.RecurrencePattern.IsRecurring && s.StartDate == date) ||

            // Recurring events - check each cron field
            (s.RecurrencePattern.IsRecurring &&
                // DayOfWeek check
                (s.RecurrencePattern.DayOfWeek == "*" ||
                 s.RecurrencePattern.DayOfWeek == "?" ||
                 s.RecurrencePattern.DayOfWeek == dayOfWeek.ToString() ||
                 s.RecurrencePattern.DayOfWeek.Contains("," + dayOfWeek + ",") ||
                 s.RecurrencePattern.DayOfWeek.StartsWith(dayOfWeek + ",") ||
                 s.RecurrencePattern.DayOfWeek.EndsWith("," + dayOfWeek) ||
                 (s.RecurrencePattern.DayOfWeek.Contains("-") &&
                  EF.Functions.Like(s.RecurrencePattern.DayOfWeek, "%-%"))) &&

                // DayOfMonth check
                (s.RecurrencePattern.DayOfMonth == "*" ||
                 s.RecurrencePattern.DayOfMonth == "?" ||
                 s.RecurrencePattern.DayOfMonth == dayOfMonth.ToString() ||
                 s.RecurrencePattern.DayOfMonth.Contains("," + dayOfMonth + ",") ||
                 s.RecurrencePattern.DayOfMonth.StartsWith(dayOfMonth + ",") ||
                 s.RecurrencePattern.DayOfMonth.EndsWith("," + dayOfMonth) ||
                 (s.RecurrencePattern.DayOfMonth.Contains("-") &&
                  EF.Functions.Like(s.RecurrencePattern.DayOfMonth, "%-%")) ||
                 s.RecurrencePattern.DayOfMonth == "L") &&

                // Month check
                (s.RecurrencePattern.Month == "*" ||
                 s.RecurrencePattern.Month == "?" ||
                 s.RecurrencePattern.Month == month.ToString() ||
                 s.RecurrencePattern.Month.Contains("," + month + ",") ||
                 s.RecurrencePattern.Month.StartsWith(month + ",") ||
                 s.RecurrencePattern.Month.EndsWith("," + month) ||
                 (s.RecurrencePattern.Month.Contains("-") &&
                  EF.Functions.Like(s.RecurrencePattern.Month, "%-%"))) &&

                // Year check
                (s.RecurrencePattern.Year == "*" ||
                 s.RecurrencePattern.Year == "?" ||
                 s.RecurrencePattern.Year == year.ToString() ||
                 s.RecurrencePattern.Year.Contains("," + year + ",") ||
                 s.RecurrencePattern.Year.StartsWith(year + ",") ||
                 s.RecurrencePattern.Year.EndsWith("," + year) ||
                 (s.RecurrencePattern.Year.Contains("-") &&
                  EF.Functions.Like(s.RecurrencePattern.Year, "%-%")))
            )
        );

        return query;
    }

    public static IQueryable<SpecialMenuScheduleEntity> ByMenu(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Action<SchedulesByMenuQuery> configureQuery)
    {
        var searchQuery = new SchedulesByMenuQuery();
        configureQuery(searchQuery);

        query = query.Where(s => s.SpecialMenuId == searchQuery.SpecialMenuId);

        if (!searchQuery.IncludeInactive)
        {
            query = query.Where(s => s.IsActive);
        }

        return query;
    }

    public static IQueryable<SpecialMenuScheduleEntity> ByVenue(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Action<SchedulesByVenueQuery> configureQuery)
    {
        var searchQuery = new SchedulesByVenueQuery();
        configureQuery(searchQuery);

        return query
            .Include(s => s.SpecialMenu)
            .Where(s => s.SpecialMenu.VenueId == searchQuery.VenueId);
    }

    public static IQueryable<SpecialMenuScheduleEntity> ForAnalytics(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Action<SchedulesForAnalyticsQuery> configureQuery)
    {
        var searchQuery = new SchedulesForAnalyticsQuery();
        configureQuery(searchQuery);

        query = query
            .Include(s => s.SpecialMenu)
                .ThenInclude(sm => sm.Venue)
            .Where(s => s.IsActive);

        if (searchQuery.UserId.HasValue)
        {
            query = query
                .Include(s => s.SpecialMenu.Venue.VenueUsers)
                .Where(s => s.SpecialMenu.Venue.VenueUsers.Any(vu =>
                    vu.UserId == searchQuery.UserId.Value && vu.IsActive));
        }

        return query;
    }
}