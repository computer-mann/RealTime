using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace realtime.Models
{
    public class TopicChatter: BaseClass
    {
        public Topic Topic { get; set; }

        [MaxLengthAttribute(183)]
        public string Message { get; set; }
    }
}