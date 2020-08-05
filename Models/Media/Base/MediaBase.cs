using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace realtime.Models.Media.Base
{
    public class MediaBase
    {
        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}