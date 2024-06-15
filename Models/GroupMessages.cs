using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RealTime.Models;

namespace realtime.Models
{
    public class GroupMessages: BaseClass
    {
        public Groups Group { get; set; }
        public int GroupId { get; set; }
        public string Message { get; set; }
        public AppUser Sender { get; set; }
        public IEnumerable<UsersInGroups> GroupMembers { get; set; }
    }
}