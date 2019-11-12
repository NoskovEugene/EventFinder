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
using EventFinder.Models.AccountModels;
using EventFinder.Models;

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
            return View();
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                Models.Entity.User user = UserRepository.Query(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                if(user != null)
                {
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
            if(ModelState.IsValid)
            {
                if(!UserRepository.Exist(u=>u.Login == model.Login)){
                    Models.Entity.User input = UserRepository.Query(u=> u.Email == model.Email).FirstOrDefault();
                    if(input == null){
                        User newUser = new User(){
                            Login = model.Login,
                            Email = model.Email,
                            Password = model.Password
                        };
                        UserRepository.Insert(newUser);
                        UserRepository.Update(newUser.Id,u=>u.UserRoles = new List<UserRole>() { new UserRole(){UserId = newUser.Id,RoleId = (int)RoleEnum.User}});
                        await Authenticate(newUser);
                        return RedirectToAction("Index","Home");
                    }
                    ModelState.AddModelError("UserExistErr","Такой пользователь уже существует");
                }
                else{
                    ModelState.AddModelError("LoginErr","Логин уже существует");
                }
            }
            return View();
        }

        public async Task Authenticate(User user)
        {
            RoleEnum role = (RoleEnum)user.UserRoles.Where(x=> x.UserId == user.Id).FirstOrDefault().RoleId;
            var claims = new List<Claim>{
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Login),
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