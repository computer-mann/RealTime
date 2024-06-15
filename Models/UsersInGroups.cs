using System.ComponentModel.DataAnnotations.Schema;
using System;
using RealTime.Models;

namespace realtime.Models
{
    public class UsersInGroups: BaseClass
    {
        public Groups Group { get; set; }
        public AppUser User { get; set; }

    }
}