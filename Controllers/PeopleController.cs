using System.Net.NetworkInformation;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using realtime.Interfaces;
using realtime.Models.DbContexts;
using realtime.Services;
using realtime.ViewModels;
using RealTime.Areas.Account.Models;

namespace realtime.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PeopleController : Controller
    {
        private readonly InMemoryCacheService inMemoryCacheService;
        private readonly RealTimeContext context;
        private readonly IRedisCache redis;
        private readonly UserManager<AppUser> userManager;
        public PeopleController(InMemoryCacheService inMemoryCacheService, IRedisCache redis,
        UserManager<AppUser> userManager, RealTimeContext context)
        {
            this.userManager = userManager;
            this.context = context;
            this.redis = redis;
            this.inMemoryCacheService = inMemoryCacheService;

        }

        [HttpGet]
        [Route("/discover")]
        public async Task<IActionResult> Discover()
        {
            var user=await inMemoryCacheService.GetOrAddUserToCache(User.Identity.Name,userManager);
            var people=await userManager.Users.Where(ip=>ip.UserName != user.UserName).ToListAsync();
            var discover=new List<DiscoverViewModel>();
             var chatted=await context.UserToUserDMs.Where(op=>op.PrincipalUserId == user.Id)
                .Select(pi=>new { pi.ChattingId ,  pi.OtherUserId }).AsNoTracking().ToListAsync(); 

            
            foreach (var person in people)
            {
                string c=null;
                var getChatId=chatted.Where(jk=>jk.OtherUserId == person.Id);
                if(getChatId.Count() > 0) c=getChatId.First().ChattingId; 
                discover.Add(new DiscoverViewModel(){
                    UserId=person.Id,
                    Avatar=person.Avatar,
                    Username=person.UserName,
                    OnlineStatus=(await redis.CheckIfOnline(person.UserName)) ? true : false,
                    ChatId = c
                });
            }
            return View(discover.OrderByDescending(op=>op.OnlineStatus));
        }
}
}