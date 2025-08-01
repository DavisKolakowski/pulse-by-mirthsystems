using Application.SpecialMenuScheduleGenerator.Enums;
using Application.SpecialMenuScheduleGenerator.Models;

using NodaTime;
using NodaTime.Text;

namespace Application.SpecialMenuScheduleGenerator;

internal class Program
{
    private static readonly LocalDatePattern DatePattern = LocalDatePattern.Create("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
    private static readonly LocalTimePattern TimePattern = LocalTimePattern.Create("HH:mm", System.Globalization.CultureInfo.InvariantCulture);

    static void Main(string[] args)
    {
        // Get start date
        Console.Write("Enter start date (yyyy-MM-dd): ");
        var startDateInput = Console.ReadLine()!;
        var startDate = DatePattern.Parse(startDateInput).Value;

        // Get start time
        Console.Write("Enter start time (HH:mm): ");
        var startTimeInput = Console.ReadLine()!;
        var startTime = TimePattern.Parse(startTimeInput).Value;

        // Get end time
        Console.Write("Enter end time (HH:mm): ");
        var endTimeInput = Console.ReadLine()!;
        var endTime = TimePattern.Parse(endTimeInput).Value;

        // Select schedule frequency
        Console.WriteLine("\nSelect schedule frequency:");
        var scheduleFrequencies = Enum.GetValues<ScheduleFrequency>().ToList();
        for (int i = 0; i < scheduleFrequencies.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scheduleFrequencies[i]}");
        }

        Console.Write("Enter your choice: ");
        var scheduleChoice = int.Parse(Console.ReadLine()!) - 1;
        var selectedScheduleFrequency = scheduleFrequencies[scheduleChoice];

        var builder = new FrequencyBuilder()
            .WithStartDate(startDate)
            .WithStartTime(startTime)
            .WithEndTime(endTime)
            .WithScheduleFrequency(selectedScheduleFrequency);

        // Handle presets vs custom
        if (selectedScheduleFrequency != ScheduleFrequency.Custom)
        {
            // Handle preset schedule frequencies
            var frequency = builder.Build();
            DisplayResult(frequency);
        }
        else
        {
            // Custom workflow
            Console.WriteLine("\nSelect custom recurrence type:");
            Console.WriteLine("1. Daily");
            Console.WriteLine("2. Weekly");
            Console.WriteLine("3. Monthly");

            Console.Write("Enter your choice: ");
            var customChoice = int.Parse(Console.ReadLine()!);

            switch (customChoice)
            {
                case 1:
                    HandleDailyCustom(builder);
                    break;
                case 2:
                    HandleWeeklyCustom(builder);
                    break;
                case 3:
                    HandleMonthlyCustom(builder);
                    break;
            }
        }
    }

    private static void HandleDailyCustom(FrequencyBuilder builder)
    {
        Console.Write("Enter interval (days): ");
        var interval = int.Parse(Console.ReadLine()!);
        builder.WithInterval(interval);

        HandleExpiration(builder);

        var frequency = builder.BuildCustomDaily();
        DisplayResult(frequency);
    }

    private static void HandleWeeklyCustom(FrequencyBuilder builder)
    {
        Console.Write("Enter interval (weeks): ");
        var interval = int.Parse(Console.ReadLine()!);
        builder.WithInterval(interval);

        // Select days of week
        Console.WriteLine("\nSelect days of week (comma-separated):");
        var daysOfWeek = Enum.GetValues<IsoDayOfWeek>().Where(d => d != IsoDayOfWeek.None).ToList();
        for (int i = 0; i < daysOfWeek.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {daysOfWeek[i]}");
        }

        Console.Write("Enter your choices: ");
        var dayChoices = Console.ReadLine()!.Split(',').Select(x => int.Parse(x.Trim()) - 1);
        var selectedDays = dayChoices.Select(i => daysOfWeek[i]).ToList();
        builder.WithDaysOfWeek(selectedDays);

        HandleExpiration(builder);

        var frequency = builder.BuildCustomWeekly();
        DisplayResult(frequency);
    }

    private static void HandleMonthlyCustom(FrequencyBuilder builder)
    {
        Console.Write("Enter interval (months): ");
        var interval = int.Parse(Console.ReadLine()!);
        builder.WithInterval(interval);

        // For custom monthly, always "OnThe" mode
        Console.WriteLine("\nSelect week position:");
        var weekIndexes = Enum.GetValues<WeekIndex>().ToList();
        for (int i = 0; i < weekIndexes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {weekIndexes[i]}");
        }

        Console.Write("Enter your choice: ");
        var indexChoice = int.Parse(Console.ReadLine()!) - 1;
        var selectedIndex = weekIndexes[indexChoice];
        builder.WithWeekIndex(selectedIndex);

        // Select day type
        Console.WriteLine("\nSelect day type:");
        var dayTypes = Enum.GetValues<Days>().Where(d =>
            d == Days.Sunday || d == Days.Monday || d == Days.Tuesday ||
            d == Days.Wednesday || d == Days.Thursday || d == Days.Friday ||
            d == Days.Saturday || d == Days.Any || d == Days.Weekday ||
            d == Days.WeekendDay).ToList();

        for (int i = 0; i < dayTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {dayTypes[i]}");
        }

        Console.Write("Enter your choice: ");
        var dayTypeChoice = int.Parse(Console.ReadLine()!) - 1;
        var selectedDayType = dayTypes[dayTypeChoice];
        builder.WithDays(selectedDayType);

        HandleExpiration(builder);

        var frequency = builder.BuildCustomMonthly();
        DisplayResult(frequency);
    }

    private static void HandleExpiration(FrequencyBuilder builder)
    {
        Console.Write("\nDoes this schedule have an expiration date? (y/n): ");
        var hasExpiration = Console.ReadLine()!.ToLower() == "y";

        if (hasExpiration)
        {
            Console.Write("Enter expiration date (yyyy-MM-dd): ");
            var expirationInput = Console.ReadLine()!;
            var expirationDate = DatePattern.Parse(expirationInput).Value;
            builder.WithExpirationDate(expirationDate);
        }
    }

    private static void DisplayResult(Frequency frequency)
    {
        Console.WriteLine("\n--- Result ---");
        Console.WriteLine($"Schedule Frequency: {frequency.Option}");
        Console.WriteLine($"Description: {frequency.Description}");
        Console.WriteLine($"Pattern Type: {frequency.Pattern.Type}");
        Console.WriteLine($"Interval: {frequency.Pattern.Interval}");

        // Show next 10 occurrences
        Console.WriteLine("\nNext 10 occurrences:");
        var occurrences = RecurrenceCalculator.GetOccurrences(frequency.Pattern, 10).ToList();
        foreach (var date in occurrences)
        {
            Console.WriteLine($"  {date:yyyy-MM-dd} at {frequency.StartTime:HH:mm}-{frequency.EndTime:HH:mm}");
        }
    }
}