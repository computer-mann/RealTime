using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;

namespace RealTime.Areas.Account.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public IFormFile Avatar { get; set; }
    }
}