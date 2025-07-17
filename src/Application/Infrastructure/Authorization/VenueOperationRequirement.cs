using System.Security.Claims;
using System.Threading.Tasks;

using Application.Contracts.Repositories;
using Application.Domain.Entities;

using Azure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Infrastructure.Authorization;

public class VenueOperationRequirement : OperationAuthorizationRequirement { }