using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFinder.Extensions;
using EventFinder.Models.Entity;
using EventFinder.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventFinder.Controllers
{
    public class ProfileController : Controller
    {
        protected IRepositoryBase<User> UserRepository { get; set; }

        public ProfileController(IRepositoryBase<User> userRepo)
        {
            this.UserRepository = userRepo;
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            var login = HttpContext.User.Identity.GetLogin();
            var user = UserRepository.Query(x => x.Login == login).FirstOrDefault();
            return View();
        }
    }
}