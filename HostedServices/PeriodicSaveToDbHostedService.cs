using Microsoft.Extensions.Hosting;
using RealTime.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealTime.HostedServices
{
    public class PeriodicSaveToDbHostedService : BackgroundService
    {
        private readonly IMessageSaver _messageSaver;
        public PeriodicSaveToDbHostedService(IMessageSaver messageSaver)
        {
            _messageSaver = messageSaver;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while (!stoppingToken.IsCancellationRequested)
            {
               if( _messageSaver.GetChannelCount() >= 41 || (await timer.WaitForNextTickAsync(stoppingToken)))
               {
                   // Save to db
               }
            }
        }
    }
}
