using Microsoft.AspNetCore.Identity;

namespace Application.Domain.Entities;

public class UserTokenEntity : IdentityUserToken<long>
{
    public UserEntity User { get; set; } = null!;
}
