using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Enums;

using NodaTime;

namespace Application.Models.Schedules;
internal class SpecialMenuScheduleDescriptionGenerator
{
    public string Get(
        Recurrence recurrence,
        LocalDate startDate,
        LocalTime startTime,
        LocalTime endTime,
        LocalDate? expirationDate,
        string? customCron
    )
    {
        string timeRange = $"{startTime.ToString("hh:mm tt", CultureInfo.InvariantCulture)} - {endTime.ToString("hh:mm tt", CultureInfo.InvariantCulture)}";
        string startDateStr = startDate.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);
        string dateRange;

        if (expirationDate.HasValue)
        {
            string endDateStr = expirationDate.Value.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);
            dateRange = $"from {startDateStr} until {endDateStr}";
        }
        else
        {
            dateRange = $"starting {startDateStr}";
        }

        switch (recurrence)
        {
            case Recurrence.Once:
                return $"Once on {startDateStr} {timeRange}";

            case Recurrence.Daily:
                return $"Daily {timeRange} {dateRange}";

            case Recurrence.Weekdays:
                return $"Weekdays (Monday to Friday) {timeRange} {dateRange}";

            case Recurrence.Weekends:
                return $"Weekends (Saturday and Sunday) {timeRange} {dateRange}";

            case Recurrence.Weekly:
                var dayOfWeek = startDate.DayOfWeek.ToString();
                return $"Weekly on {dayOfWeek} {timeRange} {dateRange}";

            case Recurrence.Monthly:
                return $"Monthly on the {startDate.Day}{GetOrdinalSuffix(startDate.Day)} {timeRange} {dateRange}";

            case Recurrence.Yearly:
                return $"Yearly on {startDate.ToString("MMMM dd", CultureInfo.InvariantCulture)} {timeRange} {dateRange}";

            case Recurrence.Custom:
                return $"Custom schedule {dateRange} between {timeRange}";

            default:
                return $"Custom schedule {dateRange} between {timeRange}";
        }
    }

    private string GetOrdinalSuffix(int number)
    {
        if (number >= 11 && number <= 13)
        {
            return "th";
        }
        switch (number % 10)
        {
            case 1: return "st";
            case 2: return "nd";
            case 3: return "rd";
            default: return "th";
        }
    }
}
