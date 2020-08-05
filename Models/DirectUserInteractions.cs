using System.ComponentModel.DataAnnotations.Schema;
using System;
using RealTime.Areas.Account.Models;

namespace realtime.Models {
    public class DirectUserInteractions : BaseClass 
    {

        //create two records for each chart initiatied to prevent complicated queries
        //like the pr || oth && oth || pr queries i will have to come up with which might be slow
       public int? LatestMessageId { get; set; }
       [ForeignKey(nameof(LatestMessageId))]
       public DirectMessages LatestDirectMessage { get; set; }
       public Guid PrincipalUserId { get; set; }
       public Guid OtherUserId {get;set;}
       
       [ForeignKey(nameof(PrincipalUserId))]
       public AppUser PrincipalUser { get; set; }
       [ForeignKey(nameof(OtherUserId))]
       public AppUser OtherUser {get;set;}
       public string ChattingId { get; set; }

    }
}