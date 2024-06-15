using System;
using Microsoft.AspNetCore.Identity;
using UuidExtensions;

namespace RealTime.Models
{
    public class AppUser : IdentityUser
    {
        public override string Id { get; set; } = Uuid7.Id25();
        public string Avatar { get; set; }

    }
}