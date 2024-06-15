using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using realtime.AllHubContexts;
using realtime.Interfaces;
using realtime.Models;
using realtime.Services;
using realtime.ViewModels;
using RealTime.Models;
using RealTime.Models.DbContexts;
using UuidExtensions;

namespace realtime.Controllers
{
    [Route ("[controller]/[action]")]
    [Authorize]
    public class DirectMessagesController : Controller
    {
        private readonly InMemoryCacheService inMemoryCacheService;
        private readonly UserManager<AppUser> userManager;
        private readonly RealTimeDbContext context;
        private readonly IHubContext<SingleChatHubContext, ISingleChatClient> hubContext;
        private readonly IRedisCache redisCache;

        public DirectMessagesController (InMemoryCacheService cacheService,
            UserManager<AppUser> userManager, RealTimeDbContext context,
            IHubContext<SingleChatHubContext, ISingleChatClient> hubContext, IRedisCache redisCache)
        {
            this.hubContext = hubContext;
            this.redisCache = redisCache;
            this.context = context;
            this.userManager = userManager;
            this.inMemoryCacheService = cacheService;

        }

        [HttpGet("/messages")]
        public async Task<IActionResult> Index ()
        {
            var user = await inMemoryCacheService.GetOrAddUserToCache (User.Identity.Name, userManager);
            var allUserMessages = await context.UserToUserDMs.Where (io => io.PrincipalUser.Id == user.Id).
            Include (ui => ui.LatestDirectMessage).ToListAsync ();

            var messagesAll = new List<AllUserMessagesViewModel> ();
            foreach (var message in allUserMessages)
            {
                messagesAll.Add (new AllUserMessagesViewModel ()
                {
                    OtherUsersName = message.OtherUser.UserName,
                        LastMessage = message.LatestDirectMessage.ActualMessage,
                        LastMessageDateTime = message.LatestDirectMessage.DateSent.ToShortDateString (),
                        Avatar = "",
                        NumberOfUnreadMessages = 0
                });
            }

            return View ("AllOfTheUsersMessages", messagesAll);
        }

        [HttpGet("/messages/chat/{userId}/{chatId}")]
        public async Task<IActionResult> ChatPage (string userId, string chatId)
        {
            if (string.IsNullOrEmpty (chatId)) return Redirect ("/discover");
            var user = inMemoryCacheService.GetUserFromCache (User.Identity.Name);
            var chatMessages = await context.DirectMessages.Where(op => op.ChattingId == chatId).AsNoTracking().ToListAsync ();
            var otherUser = await userManager.FindByIdAsync (userId);
            await inMemoryCacheService.PutUserIntoCache (otherUser);
            var dmvmodel = new SingleChatViewModel ();
                dmvmodel.UserName = otherUser.UserName;
               dmvmodel.OnlineStatus= await redisCache.CheckIfOnline(otherUser.UserName);
               dmvmodel.Messages=new List<Messages>();
               dmvmodel.ChatId=chatId;
            if (chatMessages.Count > 0)
            {
                foreach (var item in chatMessages)
                {
                    dmvmodel.Messages.Add(new Messages(){
                        Read=item.Read,
                        SentBy=(user.Id == item.SenderId) ? user.UserName : otherUser.UserName,
                        TheActualMessage=item.ActualMessage,
                        DateSent=item.DateSent
                    });
                }
            }
            return View ("SingleChat", dmvmodel);
        }

        [HttpGet("/messages/fchat/{userId?}/{chatId?}")]
        public async Task<IActionResult> ToFirstTimeDirectMessage (string userId, string chatId)
        {
            if (!string.IsNullOrWhiteSpace (chatId)) return RedirectToAction ("ChatPage", new { userId, chatId });
            if (string.IsNullOrEmpty (userId)) return LocalRedirect ("/discover");
            var user = await inMemoryCacheService.GetOrAddUserToCache (User.Identity.Name, userManager);
            var otherUser = await userManager.FindByIdAsync (userId);
            var directUserInteraction = new List<DirectUserInteractions> ();
            var chattingId = Uuid7.Id25();
            directUserInteraction.Add (new DirectUserInteractions ()
            {
                PrincipalUser = user,
                    OtherUser = otherUser,
                    ChattingId = chattingId
            });
            directUserInteraction.Add (new DirectUserInteractions ()
            {
                OtherUser = user,
                    PrincipalUser = otherUser,
                    ChattingId = chattingId
            });
            await context.UserToUserDMs.AddRangeAsync (directUserInteraction);
            await context.SaveChangesAsync ();
            var singleViewModel = new SingleChatViewModel ();
            singleViewModel.UserName = otherUser.UserName;
            singleViewModel.ChatId = chattingId;
            if (string.IsNullOrEmpty (singleViewModel.UserName)) return LocalRedirect ("/discover");
            singleViewModel.OnlineStatus = await redisCache.CheckIfOnline (otherUser.UserName);

            return View ("SingleChat", singleViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> RealtimeInOutMessages ([FromBody] InOutMessagesViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest ();
            var sender = inMemoryCacheService.GetUserFromCache (User.Identity.Name);
            var receiver = await inMemoryCacheService.GetOrAddUserToCache (model.Username, userManager);
            //await hubContext.Clients.User()
            return Ok ();
        }

    }
}