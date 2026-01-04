// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Identity;

namespace Domain
{
    // Custom role class
    public class AppRole : IdentityRole
    {
        //public const string Admin = "Admin";
        //public const string Editor = "Editor";
        //public const string User = "User";

        public AppRole() : base()
        {
        }

        public AppRole(string roleName) : base(roleName)
        {
        }

        // Add any extra role properties here
        public string? Description { get; set; }
    }

    public class MainRoles
    {
        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string User = "User";
    }
}
