using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventFinder.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EventFinder.Models.Enums;
using EventFinder.Extensions;
namespace EventFinder.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated){
                var role = User.Claims.Where(x=>x.Type == ClaimsIdentity.DefaultRoleClaimType);
                ViewBag.Role = role;
            }
            return View();
        }

        [Authorize(Roles = "Пользователь")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
