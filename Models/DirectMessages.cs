using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RealTime.Areas.Account.Models;

namespace realtime.Models
{

    //this table contains the actual messages sent by the linked users
    public class DirectMessages
    {
        [Key, DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        [MaxLengthAttribute (283)]
        public string ActualMessage { get; set; }

        [ForeignKeyAttribute (nameof (SourceID))]
        public AppUser Source { get; set; }

        [ForeignKey (nameof (TargetID))]
        public AppUser Target { get; set; }

        [MaxLength (50)]
        [Required]
        public Guid SourceID { get; set; }

        [MaxLength (50)]
        [Required]
        public Guid TargetID { get; set; }
        public bool Read { get; set; }
        public bool? DateRead { get; set; }

        public string ChattingId { get; set; }

    }
}