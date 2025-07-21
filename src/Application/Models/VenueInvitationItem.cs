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
/// Venue invitation information
/// </summary>
public class VenueInvitationItem : ItemBase
{
    /// <summary>
    /// Email address the invitation was sent to
    /// </summary>
    /// <example>newuser@example.com</example>
    [Required]
    [EmailAddress]
    [MaxLength(254, ErrorMessage = "Email cannot exceed 254 characters")]
    [JsonPropertyName("email_address")]
    [JsonPropertyOrder(1)]
    public required string EmailAddress { get; set; }

    /// <summary>
    /// Basic venue information
    /// </summary>
    [JsonPropertyName("venue")]
    [JsonPropertyOrder(2)]
    public required VenueItem Venue { get; set; }

    /// <summary>
    /// Role being offered
    /// </summary>
    [JsonPropertyName("role")]
    [JsonPropertyOrder(3)]
    public required VenueRoleItem Role { get; set; }

    /// <summary>
    /// User who sent the invitation
    /// </summary>
    [JsonPropertyName("invited_by")]
    [JsonPropertyOrder(4)]
    public required UserItem InvitedBy { get; set; }

    /// <summary>
    /// When the invitation expires
    /// </summary>
    /// <example>2025-08-21T16:51:17Z</example>
    [JsonPropertyName("expires_at")]
    [JsonPropertyOrder(5)]
    public Instant ExpiresAt { get; set; }

    /// <summary>
    /// When the invitation was accepted (if applicable)
    /// </summary>
    /// <example>2025-07-22T10:30:00Z</example>
    [JsonPropertyName("accepted_at")]
    [JsonPropertyOrder(6)]
    public Instant? AcceptedAt { get; set; }

    /// <summary>
    /// User who accepted the invitation
    /// </summary>
    [JsonPropertyName("accepted_by")]
    [JsonPropertyOrder(7)]
    public UserItem? AcceptedBy { get; set; }

    /// <summary>
    /// Whether this invitation is active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(8)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Whether this invitation has been accepted
    /// </summary>
    /// <example>false</example>
    [JsonPropertyName("is_accepted")]
    [JsonPropertyOrder(9)]
    public bool IsAccepted { get; set; }

    /// <summary>
    /// Whether this invitation is still valid (not expired and not accepted)
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_valid")]
    [JsonPropertyOrder(10)]
    public bool IsValid { get; set; }
}