using System.ComponentModel.DataAnnotations.Schema;
using System;
using RealTime.Models;

namespace realtime.Models
{
    public class DirectUserInteractions : BaseClass 
    {

        //create two records for each chart initiatied to prevent complicated queries
        //like the pr || oth && oth || pr queries i will have to come up with which might be slow
       public int? LatestMessageId { get; set; }
       [ForeignKey(nameof(LatestMessageId))]
       public DirectMessages LatestDirectMessage { get; set; }
       public AppUser PrincipalUser { get; set; }
       public AppUser OtherUser {get;set;}
       public string ChattingId { get; set; }

    }
}