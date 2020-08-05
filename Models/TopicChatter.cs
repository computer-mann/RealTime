using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace realtime.Models
{
    public class TopicChatter: BaseClass
    {
        [ForeignKeyAttribute("TopicId")]
        public Topic Topic { get; set; }
        public int TopicId { get; set; }

        [MaxLengthAttribute(183)]
        public string Message { get; set; }
    }
}