using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using realtime.Interfaces;

namespace realtime.Controllers
{
    [Route("[controller]/[action]")]
    public class GameController : Controller
    {
        private readonly IRedisCache cache;

        public GameController(IRedisCache cache)
        {
            this.cache = cache;

        }


        public async Task<IActionResult> Index()
        {
            var date = DateTime.Now.ToString();
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(1120));
            await cache.PutData("namor",date);

            return View();
        }

    }
}