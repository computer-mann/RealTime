using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTime.Areas.Account.Models;

namespace realtime.Areas.Account.Models.DbContexts
{
    public class AuthDbContext : IdentityDbContext<AppUser,IdentityRole<Guid>,Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {
            
        }

        protected  override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().Property(op=>op.Id).HasColumnType("nvarchar(256)");
        }
    }
}