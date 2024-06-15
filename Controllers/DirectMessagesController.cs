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
using realtime.Models.DbContexts;
using realtime.Services;
using realtime.ViewModels;
using RealTime.Areas.Account.Models;

namespace realtime.Controllers
{
    [Route ("[controller]/[action]")]
    [Authorize]
    public class DirectMessagesController : Controller
    {
        private readonly InMemoryCacheService inMemoryCacheService;
        private readonly UserManager<AppUser> userManager;
        private readonly RealTimeContext context;
        private readonly IHubContext<SingleChatHubContext, ISingleChatClient> hubContext;
        private readonly IRedisCache redisCache;

        public DirectMessagesController (InMemoryCacheService cacheService,
            UserManager<AppUser> userManager, RealTimeContext context,
            IHubContext<SingleChatHubContext, ISingleChatClient> hubContext, IRedisCache redisCache)
        {
            this.hubContext = hubContext;
            this.redisCache = redisCache;
            this.context = context;
            this.userManager = userManager;
            this.inMemoryCacheService = cacheService;

        }

        [HttpGet]
        [Route ("/messages")]
        public async Task<IActionResult> Index ()
        {
            var user = await inMemoryCacheService.GetOrAddUserToCache (User.Identity.Name, userManager);
            var allUserMessages = await context.UserToUserDMs.Where (io => io.PrincipalUserId == user.Id).
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

        [HttpGet]
        [Route ("/messages/chat/{userId}/{chatId}")]
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
                        SentBy=(user.Id == item.SourceId) ? user.UserName : otherUser.UserName,
                        TheActualMessage=item.ActualMessage,
                        DateSent=item.DateSent
                    });
                }
            }
            return View ("SingleChat", dmvmodel);
        }

        [HttpGet]
        [Route ("/messages/fchat/{userId?}/{chatId?}")]
        public async Task<IActionResult> ToFirstTimeDirectMessage (string userId, string chatId)
        {
            if (!string.IsNullOrWhiteSpace (chatId)) return RedirectToAction ("ChatPage", new { userId, chatId });
            if (string.IsNullOrEmpty (userId)) return LocalRedirect ("/discover");
            var user = await inMemoryCacheService.GetOrAddUserToCache (User.Identity.Name, userManager);
            var otherUser = await userManager.FindByIdAsync (userId);
            var directUserInteraction = new List<DirectUserInteractions> ();
            var chattingId = Guid.NewGuid ().ToString ().Substring (0, 4);
            directUserInteraction.Add (new DirectUserInteractions ()
            {
                PrincipalUserId = user.Id,
                    OtherUserId = otherUser.Id,
                    ChattingId = chattingId
            });
            directUserInteraction.Add (new DirectUserInteractions ()
            {
                OtherUserId = user.Id,
                    PrincipalUserId = otherUser.Id,
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