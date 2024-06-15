using System.ComponentModel.DataAnnotations.Schema;
using System;
using RealTime.Models;

namespace realtime.Models
{
    public class UsersInGroups: BaseClass
    {
        [ForeignKeyAttribute(nameof(GroupId))]
        public Groups Group { get; set; }
        public int GroupId { get; set; }
        [ForeignKeyAttribute("UserId")]
        public AppUser User { get; set; }
        public Guid UserId { get; set; }

    }
}