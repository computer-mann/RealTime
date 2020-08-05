using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using realtime.Interfaces;
using RealTime.Models;

namespace RealTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRedisCache cache;

        public HomeController(ILogger<HomeController> logger, IRedisCache cache)
        {
            this.cache = cache;
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SessId = HttpContext.Session.Id;
            ViewBag.RedisData =await cache.GetData("namor");
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult NewsFeed()
        {
            return View("Feed");
        }

        public IActionResult Privacy()
        {
            HttpContext.Session.Set("Noww", Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
