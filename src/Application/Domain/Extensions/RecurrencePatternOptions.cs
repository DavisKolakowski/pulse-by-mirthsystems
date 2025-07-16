using NodaTime;
using Application.Domain.Entities;
using Application.Domain.Constants;

namespace Application.Domain.Extensions;

public static class RecurrencePatternOptions
{
    public static RecurrencePattern Once(this SpecialMenuScheduleEntity schedule)
    {
        var minutes = schedule.StartTime.Minute.ToString();
        var hours = schedule.StartTime.Hour.ToString();
        var dayOfMonth = schedule.StartDate.Day.ToString();
        var month = schedule.StartDate.Month.ToString();
        var year = schedule.StartDate.Year.ToString();

        return new RecurrencePattern(RecurrencePatternNames.Once,
            minutes: minutes,
            hours: hours,
            dayOfMonth: dayOfMonth,
            month: month,
            year: year);
    }

    public static RecurrencePattern Daily(this SpecialMenuScheduleEntity schedule)
    {
        var minutes = schedule.StartTime.Minute.ToString();
        var hours = schedule.StartTime.Hour.ToString();

        return new RecurrencePattern(RecurrencePatternNames.Daily,
            minutes: minutes,
            hours: hours);
    }

    public static RecurrencePattern Weekdays(this SpecialMenuScheduleEntity schedule)
    {
        var minutes = schedule.StartTime.Minute.ToString();
        var hours = schedule.StartTime.Hour.ToString();

        return new RecurrencePattern(RecurrencePatternNames.Weekdays,
            minutes: minutes,
            hours: hours,
            dayOfWeek: "1-5");
    }

    public static RecurrencePattern Weekends(this SpecialMenuScheduleEntity schedule)
    {
        var minutes = schedule.StartTime.Minute.ToString();
        var hours = schedule.StartTime.Hour.ToString();

        return new RecurrencePattern(RecurrencePatternNames.Weekends,
            minutes: minutes,
            hours: hours,
            dayOfWeek: "6,0");
    }

    public static RecurrencePattern Weekly(this SpecialMenuScheduleEntity schedule)
    {
        var minutes = schedule.StartTime.Minute.ToString();
        var hours = schedule.StartTime.Hour.ToString();
        var dayOfWeek = ((int)schedule.StartDate.DayOfWeek).ToString();

        return new RecurrencePattern(RecurrencePatternNames.Weekly,
            minutes: minutes,
            hours: hours,
            dayOfWeek: dayOfWeek);
    }

    public static RecurrencePattern Monthly(this SpecialMenuScheduleEntity schedule)
    {
        var minutes = schedule.StartTime.Minute.ToString();
        var hours = schedule.StartTime.Hour.ToString();
        var dayOfMonth = schedule.StartDate.Day.ToString();

        return new RecurrencePattern(RecurrencePatternNames.Monthly,
            minutes: minutes,
            hours: hours,
            dayOfMonth: dayOfMonth);
    }

    public static RecurrencePattern Yearly(this SpecialMenuScheduleEntity schedule)
    {
        var minutes = schedule.StartTime.Minute.ToString();
        var hours = schedule.StartTime.Hour.ToString();
        var dayOfMonth = schedule.StartDate.Day.ToString();
        var month = schedule.StartDate.Month.ToString();

        return new RecurrencePattern(RecurrencePatternNames.Yearly,
            minutes: minutes,
            hours: hours,
            dayOfMonth: dayOfMonth,
            month: month);
    }

    public static RecurrencePattern Custom(
        string seconds = "*",
        string minutes = "*",
        string hours = "*",
        string dayOfMonth = "*",
        string month = "*",
        string dayOfWeek = "*",
        string year = "*")
    {
        return new RecurrencePattern(RecurrencePatternNames.Custom,
            seconds, minutes, hours, dayOfMonth, month, dayOfWeek, year);
    }
}