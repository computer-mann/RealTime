using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace realtime.Models
{
    public class BaseClass
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}