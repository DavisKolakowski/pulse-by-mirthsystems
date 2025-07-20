namespace Application.Domain.Queries;

public class SchedulesByMenuQuery
{
    public Guid SpecialMenuId { get; set; }
    public bool IncludeInactive { get; set; } = false;
}