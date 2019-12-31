using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventFinder.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EventFinder.Models.Entity;

namespace EventFinder.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EventFinderContext context;

        public HomeController(ILogger<HomeController> logger, EventFinderContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            
            if (context.Category.Count() == 0)
            {
                context.Category.AddRange(
                new Category { Name = "Танцы" },
                new Category { Name = "Музыка" },
                new Category { Name = "Фестивали" },
                new Category { Name = "Спорт" },
                new Category { Name = "Кино" },
                new Category { Name = "Настольные игры" },
                new Category { Name = "Свидания" },
                new Category { Name = "Киберспорт" },
                new Category { Name = "Путешествия" },
                new Category { Name = "Митинги" },
                new Category { Name = "Для людей XX века" },
                new Category { Name = "Совместные увлечения" }
                );
                context.SaveChanges();
            }

            if (User.Identity.IsAuthenticated){
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
