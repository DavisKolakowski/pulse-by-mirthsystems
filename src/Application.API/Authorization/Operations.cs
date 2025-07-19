using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.API.Authorization;

public static class Operations
{
    public static class Venue
    {
        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement
        {
            Name = "venue:create"
        };
        public static OperationAuthorizationRequirement Access = new OperationAuthorizationRequirement
        {
            Name = "venue:access"
        };
        public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement
        {
            Name = "venue:update"
        };
        public static OperationAuthorizationRequirement Activate = new OperationAuthorizationRequirement
        {
            Name = "venue:activate"
        };
        public static OperationAuthorizationRequirement Deactivate = new OperationAuthorizationRequirement
        {
            Name = "venue:deactivate"
        };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement
        {
            Name = "venue:delete"
        };

        public static class Users
        {
            public static OperationAuthorizationRequirement Invite = new OperationAuthorizationRequirement
            {
                Name = "venue-users:invite"
            };
            public static OperationAuthorizationRequirement Uninvite = new OperationAuthorizationRequirement
            {
                Name = "venue-users:uninvite"
            };
            public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement
            {
                Name = "venue-users:update"
            };
            public static OperationAuthorizationRequirement Remove = new OperationAuthorizationRequirement
            {
                Name = "venue-users:remove"
            };
        }

        public static class Specials
        {
            public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement
            {
                Name = "venue-specials:create"
            };
            public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement
            {
                Name = "venue-specials:update"
            };
            public static OperationAuthorizationRequirement Activate = new OperationAuthorizationRequirement
            {
                Name = "venue-specials:activate"
            };
            public static OperationAuthorizationRequirement Deactivate = new OperationAuthorizationRequirement
            {
                Name = "venue-specials:deactivate"
            };
            public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement
            {
                Name = "venue-specials:delete"
            };
        }
    }
}
