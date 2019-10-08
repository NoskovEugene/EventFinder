using EventFinder.Models.AccountModels;
using EventFinder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using Microsoft.EntityFrameworkCore.Tools;
using Microsoft.EntityFrameworkCore;
using EventFinder.Models.Enums;
using EventFinder.Extensions;
using EventFinder.Models.Entity;

namespace EventFinder.Controllers
{
    [Route("account/[action]")]
    public class AccountController : Controller
    {
        private EventFinderContext Context {get;set;}

        public AccountController(EventFinderContext context)
        {
            this.Context = context;
        }
        [Route("")]
        [ActionName("register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Route("")]
        [ActionName("login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                Models.Entity.User user = await Context.User.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if(user != null){
                    await Authenticate(user.Email);

                    return RedirectToAction("index", "Home");
                }
                ModelState.AddModelError("","Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [Route("")]
        [ActionName("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            Models.Entity.User user = await Context.User.FirstOrDefaultAsync(u => u.Email == model.Email);
            if(user == null){
                await Context.User.AddAsync(new Models.Entity.User()
                {
                    Login = model.Email,
                    Email = model.Email,
                    Password = model.Password,
                    UserRoles = new List<UserRole>()
                    {
                        
                    }
                    
                });
                await Context.SaveChangesAsync();
                await Authenticate(model.Email);
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        public async Task Authenticate(string userName)
        {
            var claims = new List<Claim>{
                new Claim(ClaimsIdentity.DefaultNameClaimType,userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,RoleEnum.User.GetDisplayName())
            };
            var identity = new ClaimsIdentity(claims,"ApplicationCookie",ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }

    }
}