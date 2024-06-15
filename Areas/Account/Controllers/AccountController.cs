using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using realtime.Services;
using RealTime.Areas.Account.ViewModels;
using RealTime.Models;

namespace realtime.Areas.Account.Controllers
{

    [Area("Account")]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signIn;
        private readonly InMemoryCacheService cacheService;

        public AccountController (UserManager<AppUser> userManager,
            SignInManager<AppUser> signIn, InMemoryCacheService cacheService)
        {
            this.cacheService = cacheService;
            this.signIn = signIn;
            this.userManager = userManager;

        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login (string returnUrl)
        {
            ViewBag.ReturnUrl=returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginVM model,string returnUrl)
        {
            if(!ModelState.IsValid) return View(model);
            var user = await userManager.FindByNameAsync (model.Login);
            if (user == null)
            {
                ModelState.AddModelError("","User doesn't exist.");
                return View (model);
            }
            var result=await signIn.PasswordSignInAsync(user,model.Password,true,false);
            if(result.Succeeded)
            {
            await cacheService.PutUserIntoCache (user);
            return string.IsNullOrEmpty(returnUrl) ? Redirect("/") : Redirect(returnUrl);
            }else
            {
                ModelState.AddModelError("","Something Went Wrong");
                return View(model);
            }
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Register (string returnUrl)
        {
            return View ();
        }

        [HttpPost]
        public async Task<IActionResult> Register (RegisterVM model)
        {
            if(!ModelState.IsValid) return View(model);
            AppUser user=new AppUser(){ UserName=model.Username};
            var result=await userManager.CreateAsync(user,model.Password);
            if(result.Succeeded)
            {
                await signIn.PasswordSignInAsync(user,model.Password,true,false);
                return Redirect("/");
            }else
            {
                return View(model);
            }
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await signIn.SignOutAsync();
            return Redirect("/");
        }
    }
}