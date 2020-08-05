using System.ComponentModel.DataAnnotations;
namespace realtime.ViewModels
{
    public class InOutMessagesViewModel
    {
        [RequiredAttribute]
        public string Username { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int ChatId { get; set; }
    }
}