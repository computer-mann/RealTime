using System;
using System.Collections.Generic;

namespace realtime.ViewModels
{
    public class SingleChatViewModel
    {
        public string UserName { get; set; }
        public bool OnlineStatus { get; set; }
        public List<Messages> Messages { get; set; }
        public string ChatId { get; set; }
    }

    public class Messages
    {
        public DateTime DateSent { get; set; }
        public bool Read { get; set; }
        public string SentBy { get; set; }
        
        public string TheActualMessage { get; set; }
    }
}