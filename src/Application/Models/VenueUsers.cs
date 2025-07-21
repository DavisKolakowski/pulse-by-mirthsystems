using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;


/// <summary>
/// Complete venue users response for displaying all users and invitations
/// </summary>
public class VenueUsers
{
    /// <summary>
    /// Currently active users with roles at this venue
    /// </summary>
    [JsonPropertyName("active_users")]
    [JsonPropertyOrder(1)]
    public List<VenueUserItem> ActiveUsers { get; set; } = new List<VenueUserItem>();

    /// <summary>
    /// Inactive users who previously had roles at this venue
    /// </summary>
    [JsonPropertyName("inactive_users")]
    [JsonPropertyOrder(2)]
    public List<VenueUserItem> InactiveUsers { get; set; } = new List<VenueUserItem>();

    /// <summary>
    /// Pending invitations that haven't been accepted yet
    /// </summary>
    [JsonPropertyName("pending_invitations")]
    [JsonPropertyOrder(3)]
    public List<VenueInvitationItem> PendingInvitations { get; set; } = new List<VenueInvitationItem>();

    /// <summary>
    /// Available roles that can be assigned to users
    /// </summary>
    [JsonPropertyName("available_roles")]
    [JsonPropertyOrder(4)]
    public List<VenueRoleItem> AvailableRoles { get; set; } = new List<VenueRoleItem>();

    /// <summary>
    /// Analytics for the venue users
    /// </summary>
    [JsonPropertyName("analytics")]
    [JsonPropertyOrder(5)]
    public VenueUsersAnalytics Analytics { get; set; } = new VenueUsersAnalytics();
}