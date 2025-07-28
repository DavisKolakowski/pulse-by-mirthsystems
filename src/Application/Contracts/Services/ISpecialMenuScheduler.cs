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
    void Schedule(SpecialMenuScheduleEntity schedule);
    void Update(SpecialMenuScheduleEntity schedule);
    void Run(Guid scheduleId);
    void Expire(Guid scheduleId);
    void Disable(Guid scheduleId);
    void Enable(Guid scheduleId);   
    void Delete(Guid scheduleId);
}
