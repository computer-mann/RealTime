using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace RealTime.Models.Interceptors
{
    public class SaveCountInterceptor : ISaveChangesInterceptor
    {
        private readonly ILogger<SaveCountInterceptor> _logger;

        public SaveCountInterceptor(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<SaveCountInterceptor>();
        }

        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("From Interceptor: Saving count of {Count} entities", eventData.Context.ChangeTracker.Entries().Count());
            return ValueTask.FromResult(result);
        }
    }
}
