using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using realtime.Services;
using RealTime.Models;
using RealTime.Models.DbContexts;

namespace realtime.Controllers
{
    public class GroupsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly InMemoryCacheService inMemoryCacheService;
        private readonly RealTimeDbContext context;
        public GroupsController(UserManager<AppUser> userManager,
        InMemoryCacheService inMemoryCacheService, RealTimeDbContext context)
        {
            this.context = context;
            this.inMemoryCacheService = inMemoryCacheService;
            this.userManager = userManager;

        }

    [HttpGet]
    [Route("/groups")]
    public async Task<IActionResult> GroupsIndex()
    {
        var user = inMemoryCacheService.GetUserFromCache(User.Identity.Name);
        var groupsJoined=await context.UsersInGroups.Where(op=>op.User.Id == user.Id).OrderBy(ui=>ui.Timestamp)
        .Include(guy=>guy.Group).ToListAsync();
        return View("Index",groupsJoined);
    }
}
}