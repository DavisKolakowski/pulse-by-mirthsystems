using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace Application.Domain.Entities;

public class UserClaimEntity : IdentityUserClaim<long>
{
    public UserEntity User { get; set; } = null!;
}
