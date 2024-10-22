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


/*
 * Add this block to nginx configuration 
    location /notificationHub {
    proxy_pass http://localhost:PORT_ON_WHICH_THE_SERVICE_RUNS/notificationHub;
    proxy_http_version 1.1;
    proxy_set_header Upgrade $http_upgrade;  
    proxy_set_header Connection "upgrade"; 
    proxy_buffering off;  
    proxy_set_header Host $host;  
    proxy_set_header X-Real-IP $remote_addr;  
    proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;  
    proxy_set_header X-Forwarded-Proto $scheme;  
    }
 */