using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using realtime.Models.DbContexts;
using realtime.Services;
using RealTime.Areas.Account.Models;

namespace realtime.Controllers
{
    public class GroupsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly InMemoryCacheService cacheService;
        private readonly RealTimeContext context;
        public GroupsController(UserManager<AppUser> userManager,
        InMemoryCacheService cacheService, RealTimeContext context)
        {
            this.context = context;
            this.cacheService = cacheService;
            this.userManager = userManager;

        }

    [HttpGet]
    [Route("/groups")]
    public async Task<IActionResult> GroupsIndex()
    {
        var user = cacheService.GetUserFromCache(User.Identity.Name);
        var groupsJoined=await context.UsersInGroups.Where(op=>op.UserId == user.Id).OrderBy(ui=>ui.Timestamp)
        .Include(guy=>guy.Group).ToListAsync();
        return View("Index",groupsJoined);
    }
}
}