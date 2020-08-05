using System.Collections.Generic;
using System.Threading.Tasks;

namespace realtime.Interfaces
{
    public interface IRedisCache
    {
        Task<List<string>> GetThoseOnline (string Key); //return usernames
        Task AddToOnline (string username);
        Task RemoveFromOnline (string username);
        Task<bool> CheckIfOnline (string username);
        Task PutData (string key, object value);
        Task PutData (string key, string value);
        Task<string> GetData (string key);
        Task PutData (string key, int value);
    }
}