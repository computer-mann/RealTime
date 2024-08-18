using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RealTime.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealTime.HostedServices
{
    public class PeriodicSaveToDbHostedService : BackgroundService
    {
        private readonly IMessageSaver _messageSaver;
        private readonly ILogger<PeriodicSaveToDbHostedService> _logger;
        public PeriodicSaveToDbHostedService(IMessageSaver messageSaver, ILogger<PeriodicSaveToDbHostedService> logger)
        {
            _messageSaver = messageSaver;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_messageSaver.GetChannelCount() < 1)
                {
                    _logger.LogInformation("No messages to save");
                    await timer.WaitForNextTickAsync(stoppingToken);
                }
                else if(_messageSaver.GetChannelCount() >= 41 || (await timer.WaitForNextTickAsync(stoppingToken)))
                {
                    _logger.LogInformation("Saving messages to db");
                   // Save to db
                   await _messageSaver.SaveMessages();
                }
                
            }
        }
    }
}
