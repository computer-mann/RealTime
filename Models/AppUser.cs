using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using UuidExtensions;

namespace RealTime.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Id = Uuid7.Id25();
        }
        public override string Id { get; set; } 
        public string Avatar { get; set; }

    }
}