using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;
public class DailyOptions
{
    private readonly LocalDate _startDate;
    private readonly FrequencyOptions _parent;
    private int _interval = 1;

    public DailyOptions(LocalDate startDate, FrequencyOptions parent)
    {
        _startDate = startDate;
        _parent = parent;
    }

    public DailyOptions Interval(int interval)
    {
        if (interval < 1)
            throw new ArgumentException("Interval must be greater than 0", nameof(interval));

        _interval = interval;
        return this;
    }

    public void Build()
    {
        _parent.SetPattern(new DailyRecurrence(_startDate, _interval));
    }

    public static implicit operator FrequencyOptions(DailyOptions options)
    {
        options.Build();
        return options._parent;
    }
}