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
        var calendar = CalendarSystem.Julian;
    }
}