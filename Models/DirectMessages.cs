using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RealTime.Models;

namespace realtime.Models
{

    //this table contains the actual messages sent by the linked users
    ///I need this to be as fast as possible so no joins, just the actual message and the users id, no joins or lookups
    public class DirectMessages
    {
        [Key, DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime? DateSent { get; set; }

        [MaxLength(283)]
        public string ActualMessage { get; set; }
        [MaxLength(25)]
        public string SenderId { get; set; }
        [MaxLength(25)]
        public string ReceipientId { get; set; }
        public bool Read { get; set; }
        public bool? DateRead { get; set; }
        [MaxLength(25)]
        public string ChattingId { get; set; }

    }
}