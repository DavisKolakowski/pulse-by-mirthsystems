using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace Application.Domain.Entities;

public class UserLoginEntity : IdentityUserLogin<long>
{
    public UserEntity User { get; set; } = null!;
}
