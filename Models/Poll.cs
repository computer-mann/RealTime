using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using RealTime.Areas.Account.Models;

namespace realtime.Models
{
    public class Poll : BaseClass
    {
        public string Question { get; set; }
        [MaxLengthAttribute(100)]
        public string OptionA { get; set; }
        [MaxLengthAttribute(100)]
        public string OptionB { get; set; }
        [MaxLengthAttribute(100)]
        public string OptionC { get; set; }
        [MaxLengthAttribute(100)]
        public string OptionD { get; set; }
        public int OptionACount { get; set; }
        public int OptionBCount { get; set; }
        public int OptionCCount { get; set; }
        public int OptionDCount { get; set; }
        [ForeignKeyAttribute("CreatorId")]
        public AppUser Creator { get; set; }
        public Guid CreatorId { get; set; }
        public string For { get; set; } //group or general
    }
}