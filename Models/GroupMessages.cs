using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RealTime.Areas.Account.Models;

namespace realtime.Models
{
    public class GroupMessages: BaseClass
    {
        [ForeignKeyAttribute(nameof(GroupId))]
        public Groups Group { get; set; }
        public int GroupId { get; set; }
        public string Message { get; set; }
        [ForeignKey("SenderId")]
        public AppUser Sender { get; set; }
        public Guid SenderId { get; set; }
        public IEnumerable<UsersInGroups> GroupMembers { get; set; }
    }
}