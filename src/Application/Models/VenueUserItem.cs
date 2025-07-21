using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using NodaTime;

namespace Application.Models;


/// <summary>
/// User associated with a venue
/// </summary>
public class VenueUserItem : ItemBase
{
    /// <summary>
    /// The user details
    /// </summary>
    [JsonPropertyName("user")]
    [JsonPropertyOrder(1)]
    public required UserItem User { get; set; }

    /// <summary>
    /// The role details
    /// </summary>
    [JsonPropertyName("role")]
    [JsonPropertyOrder(2)]
    public required VenueRoleItem Role { get; set; }

    /// <summary>
    /// Timestamp when this role was granted
    /// </summary>
    /// <example>2025-07-21T16:51:17Z</example>
    [JsonPropertyName("granted_at")]
    [JsonPropertyOrder(3)]
    public Instant GrantedAt { get; set; }

    /// <summary>
    /// User who granted this role
    /// </summary>
    [JsonPropertyName("granted_by")]
    [JsonPropertyOrder(4)]
    public UserItem? GrantedBy { get; set; }

    /// <summary>
    /// Indicates whether this role assignment is currently active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(5)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Optional notes about this role assignment
    /// </summary>
    /// <example>Promoted from staff to manager</example>
    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
    [JsonPropertyName("notes")]
    [JsonPropertyOrder(6)]
    public string? Notes { get; set; }
}