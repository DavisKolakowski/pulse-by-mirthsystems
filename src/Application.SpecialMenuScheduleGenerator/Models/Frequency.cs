using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Enums;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;

public class Frequency
{
    public ScheduleFrequency Option { get; set; }
    public required string Description { get; set; }
    public required RecurrencePattern Pattern { get; set; }
    public LocalTime StartTime { get; set; }
    public LocalTime EndTime { get; set; }
}