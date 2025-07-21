using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;


/// <summary>
/// Analytics data for venue users
/// </summary>
public class VenueUsersAnalytics
{
    /// <summary>
    /// Total number of active users
    /// </summary>
    /// <example>5</example>
    [JsonPropertyName("total_active_users")]
    [JsonPropertyOrder(1)]
    public int TotalActiveUsers { get; set; }

    /// <summary>
    /// Total number of inactive users
    /// </summary>
    /// <example>2</example>
    [JsonPropertyName("total_inactive_users")]
    [JsonPropertyOrder(2)]
    public int TotalInactiveUsers { get; set; }

    /// <summary>
    /// Number of pending invitations
    /// </summary>
    /// <example>3</example>
    [JsonPropertyName("pending_invitations_count")]
    [JsonPropertyOrder(3)]
    public int PendingInvitationsCount { get; set; }

    /// <summary>
    /// Number of expired invitations in the last 30 days
    /// </summary>
    /// <example>1</example>
    [JsonPropertyName("recently_expired_invitations")]
    [JsonPropertyOrder(4)]
    public int RecentlyExpiredInvitations { get; set; }

    /// <summary>
    /// Breakdown of users by role
    /// </summary>
    [JsonPropertyName("users_by_role")]
    [JsonPropertyOrder(5)]
    public Dictionary<string, int> UsersByRole { get; set; } = new Dictionary<string, int>();
}