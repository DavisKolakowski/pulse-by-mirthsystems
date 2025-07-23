using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;

/// <summary>
/// Paged list response wrapper
/// </summary>
/// <typeparam name="T">The type of items in the list</typeparam>
public class PagedList<T>
{
    /// <summary>
    /// The items in the current page
    /// </summary>
    [JsonPropertyName("items")]
    [JsonPropertyOrder(1)]
    public List<T> Items { get; set; } = new();

    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    /// <example>150</example>
    [JsonPropertyName("total_count")]
    [JsonPropertyOrder(2)]
    public int TotalCount { get; set; }

    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    /// <example>1</example>
    [JsonPropertyName("page")]
    [JsonPropertyOrder(3)]
    public int Page { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    /// <example>20</example>
    [JsonPropertyName("page_size")]
    [JsonPropertyOrder(4)]
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    /// <example>8</example>
    [JsonPropertyName("total_pages")]
    [JsonPropertyOrder(5)]
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;

    /// <summary>
    /// Whether there is a next page
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("has_next_page")]
    [JsonPropertyOrder(6)]
    public bool HasNextPage => Page < TotalPages;

    /// <summary>
    /// Whether there is a previous page
    /// </summary>
    /// <example>false</example>
    [JsonPropertyName("has_previous_page")]
    [JsonPropertyOrder(7)]
    public bool HasPreviousPage => Page > 1;
}
