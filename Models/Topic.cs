using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RealTime.Models;

namespace realtime.Models
{
    public class Topic : BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AppUser Creator { get; set; }
        public IEnumerable<TopicChatter> Chatter { get; set; }
    }
}