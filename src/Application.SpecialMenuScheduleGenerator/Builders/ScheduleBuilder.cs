using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Contracts;
using Application.SpecialMenuScheduleGenerator.Models;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Builders;
public class ScheduleBuilder
{
    private readonly DateTimeZone _timeZone;
    private readonly LocalDate _startDate;
    private readonly LocalTime _startTime;
    private readonly LocalTime _endTime;
    private LocalDate? _endDate;
    private IRecurrencePattern? _pattern;

    public ScheduleBuilder(DateTimeZone timeZone, LocalDate startDate, LocalTime startTime, LocalTime endTime)
    {
        _timeZone = timeZone;
        _startDate = startDate;
        _startTime = startTime;
        _endTime = endTime;
    }

    public ScheduleBuilder AddEndDate(LocalDate endDate)
    {
        _endDate = endDate;
        return this;
    }

    public ScheduleBuilder AddRecurrence(Action<RecurrenceOptions> configureRecurrence)
    {
        var options = new RecurrenceOptions(_startDate);
        configureRecurrence(options);
        _pattern = options.BuildPattern();
        return this;
    }

    public Schedule Build()
    {
        if (_pattern == null)
            throw new InvalidOperationException("Recurrence pattern must be set before building schedule");

        return new Schedule(_timeZone, _startDate, _startTime, _endTime, _endDate, _pattern);
    }
}

