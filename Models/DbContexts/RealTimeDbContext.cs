using System;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using realtime.Models;
using RealTime.Models;
using RealTime.Models.Interceptors;

namespace RealTime.Models.DbContexts
{
    public class RealTimeDbContext : IdentityDbContext<AppUser>
    {
        private readonly ILoggerFactory _logger;
        public RealTimeDbContext(DbContextOptions<RealTimeDbContext> options, ILoggerFactory logger) : base(options)
        {
            _logger = logger;
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
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.AddInterceptors(new SaveCountInterceptor(_logger));
        }

    }
}