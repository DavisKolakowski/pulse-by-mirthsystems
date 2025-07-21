namespace Application.Queries;

public class SpecialsByMenuQuery
{
    public Guid SpecialMenuId { get; set; }
    public bool IncludeInactive { get; set; } = false;
}