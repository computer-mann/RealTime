using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using realtime.Interfaces;
using StackExchange.Redis;

namespace realtime.Services
{
    public class RedisCacheService : IRedisCache
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database=_connectionMultiplexer.GetDatabase(0);

        }
        public async Task<string> GetData(string key)
        {
            
            return await _database.StringGetAsync(key);
        }

        public async Task PutData(string key, string value)
        {
            
            await _database.StringSetAsync(key,value,TimeSpan.FromSeconds(90));
        }

        public Task PutData(string key, int value)
        {
            throw new System.NotImplementedException();
        }

        public Task PutData(string key, object value)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddToOnline(string username)
        {
            if(!(await _database.SetContainsAsync("users:online", username)))
            {
                 await _database.SetAddAsync("users:online",username);
            }
            
        }

        public async Task<bool> CheckIfOnline(string username)
        {
            return (await _database.SetContainsAsync("users:online",username));
        }

        public Task<List<string>> GetThoseOnline(string Key)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFromOnline(string username)
        {
            await _database.SetRemoveAsync("users:online",username);
        }
    }
}