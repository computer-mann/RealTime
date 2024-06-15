using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using RealTime.Models;

namespace realtime.Services
{
    public class InMemoryCacheService
    {
        private readonly IMemoryCache cache;

        private readonly MemoryCacheEntryOptions options;

        public InMemoryCacheService (IMemoryCache serve)
        {

            cache = serve;
            options = new MemoryCacheEntryOptions ()
            {
                SlidingExpiration = TimeSpan.FromMinutes (7),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes (15)
            };
        }

        public Task PutUserIntoCache (AppUser user)
        {
            cache.Set($"{user.UserName}-auth", user, options);
            return Task.CompletedTask;

        }

        public AppUser GetUserFromCache (string usernameFromHttpContext)
        {
            AppUser user = null;
            cache.TryGetValue ($"{usernameFromHttpContext}-auth", out user);

            return user;
        }

        public async Task<AppUser> GetOrAddUserToCache (string usernameFromHttp, UserManager<AppUser> manager)
        {

            AppUser user1 = await cache.GetOrCreateAsync ($"{usernameFromHttp}-auth", async options =>
            {
                options.SetOptions (this.options);

                return await manager.FindByNameAsync (usernameFromHttp);

            });
            return user1;

        }
        public async Task<AppUser> GetOrAddUserIdToCache(string usernameFromHttp, UserManager<AppUser> manager)
        {
            
            AppUser user1 = await cache.GetOrCreateAsync($"{usernameFromHttp}-auth", async options =>
           {
               options.SetOptions(this.options);

               return await manager.FindByNameAsync(usernameFromHttp);

           });
            return user1;

        }

        public void RemoveUserFromCache (string username)
        {
            cache.Remove ($"{username}-auth");

        }
    }
}
