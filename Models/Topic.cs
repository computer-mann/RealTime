using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RealTime.Areas.Account.Models;

namespace realtime.Models {
    public class Topic : BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        [ForeignKeyAttribute("CreatorId")]
        public AppUser Creator { get; set; }
        public Guid CreatorId { get; set; }

        public IEnumerable<TopicChatter> Chatter { get; set; }
    }
}