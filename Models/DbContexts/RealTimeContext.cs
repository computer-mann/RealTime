using Microsoft.EntityFrameworkCore;
using RealTime.Areas.Account.Models;

namespace realtime.Models.DbContexts
{
    public class RealTimeContext : DbContext
    {
        public RealTimeContext (DbContextOptions<RealTimeContext> options) : base (options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<DirectMessages> DirectMessages { get; set; } //1
        public DbSet<GroupMessages> GroupMessages { get; set; } //2
        public DbSet<Poll> Polls { get; set; } //3
        public DbSet<Topic> Topics { get; set; } //4
        public DbSet<TopicChatter> TopicChatters { get; set; } //5
        public DbSet<UsersInGroups> UsersInGroups { get; set; } //6
        public DbSet<DirectUserInteractions> UserToUserDMs { get; set; }//7
        public DbSet<Groups> Groups { get; set; } //8

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating (builder);
            builder.Entity<Groups>();

            /* builder.HasSequence<int> ("sqChattingId");
            builder.Entity<DirectUserInteractions> ().Property (o => o.ChattingId)
                .HasDefaultValueSql ("Next Value for sqChattingId"); */
            
            builder.Ignore<AppUser> ();

        }
    }
}