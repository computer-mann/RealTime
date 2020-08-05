using System.ComponentModel.DataAnnotations;

namespace RealTime.Areas.Account.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}