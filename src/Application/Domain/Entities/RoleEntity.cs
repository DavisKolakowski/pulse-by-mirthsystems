using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace Application.Domain.Entities;

public class RoleEntity : IdentityRole<long>
{
    [Column("description")]
    [Required]
    public string Description { get; set; } = null!;

    public List<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    public List<RoleClaimEntity> RoleClaims { get; set; } = new List<RoleClaimEntity>();
}
