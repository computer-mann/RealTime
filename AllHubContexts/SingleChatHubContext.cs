using System;
using System.IO.Pipes;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using realtime.Interfaces;
using realtime.Models;
using realtime.Services;
using RealTime.Models;
using RealTime.Models.DbContexts;

namespace realtime.AllHubContexts
{
    [Authorize]
    public class SingleChatHubContext : Hub<ISingleChatClient>
    {
        private readonly ILogger<SingleChatHubContext> logger;
        private readonly InMemoryCacheService cacheService;
        private readonly UserManager<AppUser> userManager;

        private readonly IRedisCache redisCache;
        private readonly RealTimeDbContext dbcontext;

        public SingleChatHubContext (ILogger<SingleChatHubContext> logger,
            InMemoryCacheService cacheService, UserManager<AppUser> userManager,
            IRedisCache redisCache, RealTimeDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
            this.redisCache = redisCache;
            this.userManager = userManager;
            this.cacheService = cacheService;
            this.logger = logger;

        }

        public async Task SendRealTimeMessage (string username,string chatId, string message)
        {
            logger.LogWarning("{chatId}",chatId);
            var receiverUserId = (await cacheService.GetOrAddUserToCache(username, userManager)).Id;
            await Clients.User(receiverUserId.ToString()).ReceiveMessage(message);
            var senderUserId=(cacheService.GetUserFromCache(Context.User.Identity.Name)).Id;
            await dbcontext.DirectMessages.AddAsync(new DirectMessages(){
                ActualMessage=message,
                SourceId=senderUserId,
                TargetId=receiverUserId,
                DateSent=DateTime.Now,
                ChattingId=chatId
            });
            await dbcontext.SaveChangesAsync();
        }

        public override async Task OnConnectedAsync ()
        {
            await redisCache.AddToOnline (Context.User.Identity.Name);
            //await Clients.Others.WelcomeAlert($"{Context.User.Identity.Name} has connected.");
            await base.OnConnectedAsync ();
        }
        public override async Task OnDisconnectedAsync (Exception ex)
        {
            await redisCache.RemoveFromOnline (Context.User.Identity.Name);
            logger.LogWarning ("Logging OnDisconnectedAsync out");
            await base.OnDisconnectedAsync (ex);
        }
    }
}