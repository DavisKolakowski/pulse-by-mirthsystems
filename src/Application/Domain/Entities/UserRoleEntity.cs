using Microsoft.AspNetCore.Identity;

namespace Application.Domain.Entities;

public class UserRoleEntity : IdentityUserRole<long>
{
    public UserEntity User { get; set; } = null!;
    public RoleEntity Role { get; set; } = null!;
}
