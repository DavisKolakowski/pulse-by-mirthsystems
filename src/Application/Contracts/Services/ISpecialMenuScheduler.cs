using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Entities;
using Application.Models;
using Application.Models.Requests;

namespace Application.Contracts.Services;


public interface ISpecialMenuScheduler
{
    Task AddOrUpdate(SpecialMenuScheduleEntity schedule);
    Task Remove(Guid scheduleId);
}
