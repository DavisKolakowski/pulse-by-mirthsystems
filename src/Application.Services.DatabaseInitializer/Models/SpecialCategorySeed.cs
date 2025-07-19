namespace Application.Services.DatabaseInitializer.Models;

public class SpecialCategorySeed
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public required int SortOrder { get; set; }
}
