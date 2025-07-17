using System.Security.Claims;
using System.Threading.Tasks;

using Application.Contracts.Repositories;
using Application.Domain.Entities;

using Azure;

using Microsoft.AspNetCore.Authorization;

namespace Application.Infrastructure.Authorization;

public static class Operations
{
    public static class Venues
    {
        public static VenueOperationRequirement Create = new() { Name = nameof(Create) };
        public static VenueOperationRequirement Read = new() { Name = nameof(Read) };
        public static VenueOperationRequirement Update = new() { Name = nameof(Update) };
        public static VenueOperationRequirement Activate = new() { Name = nameof(Activate) };
        public static VenueOperationRequirement Deactivate = new() { Name = nameof(Deactivate) };
        public static VenueOperationRequirement Delete = new() { Name = nameof(Delete) };
    }

    public static class Users
    {
        public static VenueOperationRequirement Invite = new() { Name = nameof(Invite) };
        public static VenueOperationRequirement Uninvite = new() { Name = nameof(Uninvite) };
        public static VenueOperationRequirement Update = new() { Name = nameof(Update) };
        public static VenueOperationRequirement Remove = new() { Name = nameof(Remove) };
    }

    public static class Menus
    {
        public static VenueOperationRequirement Create = new() { Name = nameof(Create) };
        public static VenueOperationRequirement Read = new() { Name = nameof(Read) };
        public static VenueOperationRequirement Update = new() { Name = nameof(Update) };
        public static VenueOperationRequirement Activate = new() { Name = nameof(Activate) };
        public static VenueOperationRequirement Deactivate = new() { Name = nameof(Deactivate) };
        public static VenueOperationRequirement Delete = new() { Name = nameof(Delete) };
    }
}