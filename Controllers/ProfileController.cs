using System;
using System.Data.Entity;
using System.Linq;
using EventFinder.Extensions;
using EventFinder.Models;
using EventFinder.Models.Entity;
using EventFinder.Models.ProfileModels;
using EventFinder.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventFinder.Controllers
{
    [Authorize()]
    public class ProfileController : Controller
    {
        protected IRepositoryBase<User> UserRepository { get; set; }

        protected IRepositoryBase<Event> EventRepository { get; set; }

        protected IRepositoryBase<Forum> ForumRepository { get; set; }

        private readonly EventFinderContext context;

        public ProfileController(IRepositoryBase<Forum> forumRepo, 
                                    IRepositoryBase<User> userRepo,
                                    IRepositoryBase<Event> eventRepo,
                                    EventFinderContext context)
        {
            this.UserRepository = userRepo;
            this.EventRepository = eventRepo;
            this.context = context;
            this.ForumRepository = forumRepo;
        }

        [Route("profile/")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("profile/info")]
        public IActionResult Info()
        {
            var login = HttpContext.User.Identity.GetLogin();
            var user = UserRepository.Query(x => x.Login == login).FirstOrDefault();
            var model = new ProfileViewModel { User = user };
            return PartialView(model);
        }
        [Route("profile/participation")]
        public IActionResult Participation()
        {
            var login = HttpContext.User.Identity.GetLogin();
            var id = UserRepository.Query(x => x.Login == login).Select(s => s.Id).FirstOrDefault();
            var events = context.EventUser.Include("Event").Where(s => s.UserId == id).Select(s=>s.Event).ToList();
            return PartialView("Events", events);
        }
        [Route("profile/myevent")]
        public IActionResult MyEvent()
        {
            var login = HttpContext.User.Identity.GetLogin();
            var id = UserRepository.Query(x => x.Login == login).Select(s => s.Id).FirstOrDefault();
            var events = EventRepository.Query(x => x.OwnerId == id).ToList();
            return PartialView(events);
        }

        [Route("profile/myeventdelete/{id}")]
        public IActionResult MyEventDelete(int id)
        {
            EventRepository.Delete(id);
            var login = HttpContext.User.Identity.GetLogin();
            var userId = UserRepository.Query(x => x.Login == login).Select(s => s.Id).FirstOrDefault();
            var events = EventRepository.Query(x => x.OwnerId == userId).ToList();
            return PartialView("MyEvent", events);
        }

        [Route("profile/mydiscussion")]
        public IActionResult MyDiscussion()
        {
            var login = HttpContext.User.Identity.GetLogin();
            var id = UserRepository.Query(x => x.Login == login).Select(s => s.Id).FirstOrDefault();
            var forums = ForumRepository.Query(x => x.OwnerId == id).ToList();
            return PartialView(forums);
        }

        [Route("profile/mydiscussiondelete/{id}")]
        public IActionResult MyDiscussionDelete(int id)
        {
            ForumRepository.Delete(id);
            var login = HttpContext.User.Identity.GetLogin();
            var userId = UserRepository.Query(x => x.Login == login).Select(s => s.Id).FirstOrDefault();
            var forums = ForumRepository.Query(x => x.OwnerId == userId).ToList();
            return PartialView("MyDiscussion", forums);
        }

        [HttpGet]
        [Route("profile/editprofile")]
        public IActionResult EditProfile()
        {
            var login = HttpContext.User.Identity.GetLogin();
            var user = UserRepository.Query(x => x.Login == login).FirstOrDefault();
            return PartialView(user);
        }

        [HttpPost]
        [Route("profile/editprofile")]
        public IActionResult EditProfile(User model)
        {
            if (ModelState.IsValid)
            {
                model.Password = UserRepository.Query(s => s.Id == model.Id).Select(s => s.Password).FirstOrDefault();
                model.Login = UserRepository.Query(s => s.Id == model.Id).Select(s => s.Login).FirstOrDefault();
                UserRepository.UpdateEntity(model);
            }
                
            return RedirectToAction("Profile", "Profile");
        }
    }
}