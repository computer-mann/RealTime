using System;
using Microsoft.AspNetCore.Identity;

namespace RealTime.Areas.Account.Models {
    public class AppUser : IdentityUser<Guid> {
        
        public string Avatar { get; set; }

    }
}