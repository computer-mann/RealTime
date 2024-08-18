using Microsoft.Extensions.DependencyInjection;
using realtime.Models;
using RealTime.Models.DbContexts;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RealTime.Services
{
    public class MessageSaver : IMessageSaver
    {
        private readonly ChannelWriter<DirectMessages> _writer;
        private readonly IServiceScopeFactory _factory;
        
        private Channel<DirectMessages> _channel = Channel.CreateBounded<DirectMessages>(new BoundedChannelOptions(100)
        {
            AllowSynchronousContinuations = false,
            SingleReader = true,
            SingleWriter = false,
            FullMode = BoundedChannelFullMode.DropOldest
            
        });

        public MessageSaver(IServiceScopeFactory factory)
        {
            _factory = factory;
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
        }

        public async Task SaveMessages()
        {
            using var scope = _factory.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<RealTimeDbContext>();
            while (await _channel.Reader.WaitToReadAsync())
            {
                while (_channel.Reader.TryRead(out var message))
                {
                    await dbcontext.DirectMessages.AddAsync(message);
                }
            }
            await dbcontext.SaveChangesAsync();
            

        }
        public int GetChannelCount()
        {
            return _channel.Reader.Count;
        }
    }

    public interface IMessageSaver
    {
        public Task SaveMessages();
        public Task AddMessageToChannel(DirectMessages messages);
        public int GetChannelCount();
    }
}
