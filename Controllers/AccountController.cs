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
using EventFinder.Models.Repositories;

namespace EventFinder.Controllers
{
    [Route("account/[action]")]
    public class AccountController : Controller
    {
        private IRepositoryBase<User> UserRepository {get;set;}
        public AccountController(EventFinderContext context,IRepositoryBase<User> userRepository)
        {
            this.UserRepository = userRepository;
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
            if (User.Identity.IsAuthenticated)
            {
                var role = User.Claims.Where(x=> x.Type == ClaimsIdentity.DefaultRoleClaimType);
                ViewBag.Role = role;
            }
            return View();
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                Models.Entity.User user = UserRepository.Query(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                if(user != null){
                    await Authenticate(user);

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
            Models.Entity.User input = UserRepository.Query(u => u.Email == model.Email).FirstOrDefault();
            if(input == null)
            {
                User newUser = new User(){
                    Login = model.Email,
                    Email = model.Email,
                    Password = model.Password
                };
                UserRepository.Insert(newUser);
                User tmp = UserRepository.Query(u => u.Email == newUser.Email).FirstOrDefault();
                UserRepository.Update(tmp.Id,v=>v.UserRoles = new List<UserRole>(){ new UserRole() { UserId = tmp.Id,RoleId = (int)RoleEnum.User } });
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

        public async Task Authenticate(User user)
        {
            RoleEnum role = (RoleEnum)user.UserRoles.Where(x=> x.UserId == user.Id).FirstOrDefault().RoleId;
            var claims = new List<Claim>{
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,role.GetDisplayName())
            };
            var identity = new ClaimsIdentity(claims,"ApplicationCookie",ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }

    }
}