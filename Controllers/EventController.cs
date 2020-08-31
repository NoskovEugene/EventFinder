using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFinder.Extensions;
using EventFinder.Models;
using EventFinder.Models.Entity;
using EventFinder.Models.EventModels;
using EventFinder.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFinder.Controllers
{
    [Authorize()]
    public class EventController : Controller
    {
        protected IRepositoryBase<User> UserRepository { get; set; }
        protected IRepositoryBase<Event> EventRepository { get; set; }
        protected IRepositoryBase<Category> CategoryRepository { get; set; }
        protected IRepositoryBase<Forum> ForumRepository { get; set; }

        private readonly EventFinderContext context;
        public EventController(IRepositoryBase<Forum> forumRepo, 
                                IRepositoryBase<Event> eventRepo,
                               IRepositoryBase<User> userRepo,
                               IRepositoryBase<Category> catRepo,
                               EventFinderContext context)
        {
            this.ForumRepository = forumRepo;
            this.EventRepository = eventRepo;
            this.UserRepository = userRepo;
            this.CategoryRepository = catRepo;
            this.context = context;
        }

        [Route("events")]
        public IActionResult Events()
        {
            var events = EventRepository.Query(s => s.Id != 0).ToList();

            return View(events);
        }

        [Route("events/{id}")]
        public IActionResult EventDetails(int id)
        {
            var login = HttpContext.User.Identity.GetLogin();
            var idUser = context.User.Where(x => x.Login == login).Select(s => s.Id).FirstOrDefault();
            var ue = context.EventUser.Where(s => s.EventId == id).ToList();
            var e = EventRepository.Query(s => s.Id == id).FirstOrDefault();
            var model = new EventDetailsViewModel() { Event = e, EventUser = ue };

            return View(model);
        }

        [HttpGet]
        [Route("events/createevent")]
        public IActionResult CreateEvent()
        {
            var model = new CreateEventModel { Category = CategoryRepository.Query(s => s.Id != 0).ToList() };
            return View(model);
        }

        [HttpPost]
        [Route("events/createevent")]
        public IActionResult CreateEvent(CreateEventModel model)
        {
            if (ModelState.IsValid)
            {
                var login = HttpContext.User.Identity.GetLogin();
                var user = UserRepository.Query(x => x.Login == login).FirstOrDefault();
                EventRepository.Insert(new Event()
                {
                    CreationDate = DateTime.Now,
                    OwnerId = user.Id,
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    EventDate = model.EventDate,
                    Place = model.Place,
                    Description = model.Description,
                    EventLink = model.EventLink

                });
                if (model.CreateChat == true)
                {
                    ForumRepository.Insert(new Forum()
                    {
                        CreationTime = DateTime.Now,
                        OwnerId = user.Id,
                        Theme = model.Name,
                        CategoryId = model.CategoryId,
                        EventId = EventRepository.Query(s => s.Name == model.Name && s.OwnerId == user.Id).Select(s => s.Id).FirstOrDefault()
                    });
                }
                return RedirectToAction("Events", "Event");
            }
            else
            {
                model.Category = CategoryRepository.Query(s => s.Id != 0).ToList();
                return View(model);
            }
        }

    }
}