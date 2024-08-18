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
using RealTime.Services;

namespace realtime.AllHubContexts
{
    [Authorize]
    public class SingleChatHubContext : Hub<ISingleChatClient>
    {
        private readonly ILogger<SingleChatHubContext> logger;
        private readonly InMemoryCacheService cacheService;
        private readonly UserManager<AppUser> userManager;

        private readonly IRedisCache redisCache;
        private readonly IMessageSaver _messageSaver;

        public SingleChatHubContext (ILogger<SingleChatHubContext> logger,
            InMemoryCacheService cacheService, UserManager<AppUser> userManager,
            IRedisCache redisCache, IMessageSaver messageSaver)
        {
            _messageSaver = messageSaver;
            this.redisCache = redisCache;
            this.userManager = userManager;
            this.cacheService = cacheService;
            this.logger = logger;

        }

        public async Task SendRealTimeMessage (string receiverUsername,string chatId, string message)
        {
            //check if and how i can send files to receiver
            logger.LogInformation("{chatId}",chatId);
            var receiverUserId = (await cacheService.GetOrAddUserToCache(receiverUsername, userManager)).Id;
            await Clients.User(receiverUserId.ToString()).ReceiveMessage(message);
            var senderUserId=(cacheService.GetUserFromCache(Context.User.Identity.Name)).Id;
            //need to send this to a channelwriter
            //only save to the db when the channel has 40 messages or every 5 seconds
            await _messageSaver.AddMessageToChannel(new DirectMessages(){
                ActualMessage=message,
                SenderId=senderUserId,
                ReceipientId=receiverUserId,
                DateSent=DateTime.UtcNow,
                ChattingId=chatId
            });
        }

        public override async Task OnConnectedAsync ()
        {
            await redisCache.AddToOnline (Context.User.Identity.Name);
            //await Clients.Others.WelcomeAlert($"{Context.User.Identity.Name} has connected.");
            await base.OnConnectedAsync ();
        }
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await redisCache.RemoveFromOnline (Context.User.Identity.Name);
            await base.OnDisconnectedAsync(ex);
        }
    }
}