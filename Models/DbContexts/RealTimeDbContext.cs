using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using realtime.Models;
using RealTime.Models;

namespace RealTime.Models.DbContexts
{
    public class RealTimeDbContext : IdentityDbContext<AppUser, IdentityRole<string>, string>
    {
        public RealTimeDbContext(DbContextOptions<RealTimeDbContext> options) : base(options)
        {

        }
        public DbSet<DirectMessages> DirectMessages { get; set; } //1
        public DbSet<GroupMessages> GroupMessages { get; set; } //2
        public DbSet<Poll> Polls { get; set; } //3
        public DbSet<Topic> Topics { get; set; } //4
        public DbSet<TopicChatter> TopicChatters { get; set; } //5
        public DbSet<UsersInGroups> UsersInGroups { get; set; } //6
        public DbSet<DirectUserInteractions> UserToUserDMs { get; set; }//7
        public DbSet<Groups> Groups { get; set; } //8

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().Property(op => op.Id).HasColumnType("varchar(256)");
        }
    }
}