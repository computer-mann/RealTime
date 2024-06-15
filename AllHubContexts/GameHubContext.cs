using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using realtime.Interfaces;

namespace realtime.AllHubContexts {
    public class GameHubContext : Hub<IGameClient>
     {
        private readonly ILogger<GameHubContext> logger;
        public GameHubContext (ILogger<GameHubContext> logger)
         {
            this.logger = logger;

        }

        public async Task SendCoordinates (int x, int y)
        {
            logger.LogInformation("user is {name}", Context.User.Identity.Name);
            logger.LogInformation("connectionId is {connectionId}", Context.ConnectionId);
            logger.LogInformation("userIdentifier is {identifier}", Context.UserIdentifier);
           
            await Clients.Others.GetCoordinates(x, y);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.WelcomeAlert($"{Context.User.Identity.Name} has connected.");
            await base.OnConnectedAsync();
        }
    }
}