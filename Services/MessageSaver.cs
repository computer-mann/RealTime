using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using realtime.Models;
using RealTime.Models.DbContexts;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RealTime.Services
{
    public class MessageSaver : IMessageSaver
    {
        private readonly ChannelWriter<DirectMessages> _writer;
        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<MessageSaver> _logger;
        
        private Channel<DirectMessages> _channel = Channel.CreateBounded<DirectMessages>(new BoundedChannelOptions(100)
        {
            AllowSynchronousContinuations = false,
            SingleReader = true,
            SingleWriter = false,
            FullMode = BoundedChannelFullMode.DropOldest
            
        });

        public MessageSaver(IServiceScopeFactory factory, ILogger<MessageSaver> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task AddMessageToChannel(DirectMessages message)
        {
            while(await _channel.Writer.WaitToWriteAsync())
            {
                if (_channel.Writer.TryWrite(message))
                {
                    break;
                }
            }
            _logger.LogInformation("Message added to channel");
        }

        public async Task SaveMessages(int numOfMessagesToSave)
        {
            using var scope = _factory.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<RealTimeDbContext>();
            for(int i=0;i < numOfMessagesToSave; i++)
            {
                dbcontext.DirectMessages.Add(await _channel.Reader.ReadAsync());
            } 
            if((await dbcontext.SaveChangesAsync())> 0)
            {
                _logger.LogInformation("Channel Messages saved to db");
            }
            else
            {
                _logger.LogInformation("no thing to save as channel is empty");
            }
            

        }
        public int GetChannelCount()
        {
            return _channel.Reader.Count;
        }
    }

    public interface IMessageSaver
    {
        public Task SaveMessages(int numOfMessagesToSave);
        public Task AddMessageToChannel(DirectMessages messages);
        public int GetChannelCount();
    }
}
