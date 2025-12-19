// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Identity;

namespace Domain
{
    // Custom role class
    public class AppRole : IdentityRole
    {
        // Add any extra role properties here
        public string? Description { get; set; }
    }
}
