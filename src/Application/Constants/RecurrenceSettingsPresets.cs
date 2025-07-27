using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Models.Schedules;
using Application.Utilities;

namespace Application.Constants;
public static class RecurrenceSettingsPresets
{
    public static class Once
    {
        public const string Name = "once";
    }
    public static class Daily
    {
        public const string Name = "daily";
    }
    public static class Weekdays
    {
        public const string Name = "weekdays";
    }
    public static class Weekends
    {
        public const string Name = "weekends";
    }
    public static class Weekly
    {
        public const string Name = "weekly";
    }
    public static class Monthly
    {
        public const string Name = "monthly";
    }
    public static class Yearly
    {
        public const string Name = "yearly";
    }
    public static class Custom
    {
        public const string Name = "custom";
    }
}
