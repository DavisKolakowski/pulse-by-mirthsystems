using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

using NodaTime;

namespace Application.Domain.Entities;

public class UserEntity : IdentityUser<long>
{
    public List<UserClaimEntity> Claims { get; set; } = new List<UserClaimEntity>();
    public List<UserLoginEntity> Logins { get; set; } = new List<UserLoginEntity>();
    public List<UserTokenEntity> Tokens { get; set; } = new List<UserTokenEntity>();
    public List<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    public List<VenueUserRoleEntity> VenueRoles { get; set; } = new List<VenueUserRoleEntity>();
    public List<VenueInvitationEntity> SentInvitations { get; set; } = new List<VenueInvitationEntity>();
    public List<VenueInvitationEntity> ReceivedInvitations { get; set; } = new List<VenueInvitationEntity>();
}
