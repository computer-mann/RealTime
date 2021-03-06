using System.ComponentModel.DataAnnotations.Schema;
using System;
using RealTime.Areas.Account.Models;
using System.Collections.Generic;

namespace realtime.Models
{
    public class Groups: BaseClass
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        
        public int NumberOfMembers { get; set; }
        public string Image { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKeyAttribute("CreatorId")]
        public AppUser Creator { get; set; }
        public Guid CreatorId { get; set; }
        public List<UsersInGroups> GroupMembers { get; set; }

    }
}