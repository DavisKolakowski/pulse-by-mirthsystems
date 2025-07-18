﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IVenueUserRoleRepository
{
    Task<VenueUserRoleEntity?> GetVenueRoleForUserAsync(Guid userId, Guid venueId, CancellationToken cancellationToken = default);
}
