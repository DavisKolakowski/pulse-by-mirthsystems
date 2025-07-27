using System;
using System.ComponentModel.DataAnnotations;

using Application.Constants;
using Application.Enums;

namespace Application.Models.Schedules;

public class RecurrenceSettings
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public CronPattern CronPattern { get; set; } = null!;
}