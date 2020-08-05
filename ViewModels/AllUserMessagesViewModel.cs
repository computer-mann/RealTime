namespace realtime.ViewModels
{
    public class AllUserMessagesViewModel
    {
        public string Avatar { get; set; }
        public string OtherUsersName { get; set; }
        public string LastMessage { get; set; }
        public string LastMessageDateTime { get; set; }
        public int NumberOfUnreadMessages { get; set; }
    }
}