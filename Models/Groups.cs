using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using RealTime.Models;

namespace realtime.Models
{
    public class Groups: BaseClass
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        
        public int NumberOfMembers { get; set; }
        public string Image { get; set; }
        public DateTime DateCreated { get; set; }
        public AppUser Creator { get; set; }
        public List<UsersInGroups> GroupMembers { get; set; }

    }
}