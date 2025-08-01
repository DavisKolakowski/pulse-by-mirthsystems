using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Enums;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Contracts;

public interface IRecurrencePattern
{
    IEnumerable<LocalDate> GenerateDates(LocalDate? until = null);
    string GetDescription();
    RecurrencePatternType Type { get; }
}
