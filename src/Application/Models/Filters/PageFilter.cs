using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Filters;

/// <summary>
/// Base pagination filter for all paged requests
/// </summary>
public abstract class PageFilter
{
    /// <summary>
    /// Page number (1-based)
    /// </summary>
    /// <example>1</example>
    [JsonPropertyName("page")]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Items per page
    /// </summary>
    /// <example>20</example>
    [JsonPropertyName("page_size")]
    public int PageSize { get; set; } = 20;
}
