using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFinder.Extensions;
using EventFinder.Models;
using EventFinder.Models.Entity;
using EventFinder.Models.EventModels;
using EventFinder.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFinder.Controllers
{
    public class EventController : Controller
    {
        protected IRepositoryBase<User> UserRepository { get; set; }
        protected IRepositoryBase<Event> EventRepository { get; set; }
        protected IRepositoryBase<Category> CategoryRepository { get; set; }

        private readonly EventFinderContext context;
        public EventController(IRepositoryBase<Event> eventRepo,
                               IRepositoryBase<User> userRepo,
                               IRepositoryBase<Category> catRepo,
                               EventFinderContext context)
        {
            this.EventRepository = eventRepo;
            this.UserRepository = userRepo;
            this.CategoryRepository = catRepo;
            this.context = context;
        }

        [Route("Events/")]
        public IActionResult Events()
        {
            var events = EventRepository.Query(s => s.Id != 0).ToList();

            return View(events);
        }

        [Route("Events/{id}")]
        public IActionResult EventDetails(int id)
        {
            var login = HttpContext.User.Identity.GetLogin();
            var idUser = context.User.Where(x => x.Login == login).Select(s => s.Id).FirstOrDefault();
            var ue = context.EventUser.Where(s => s.EventId == id && s.UserId == idUser).FirstOrDefault();
            var e = EventRepository.Query(s => s.Id == id).FirstOrDefault();
            var model = new EventDetailsViewModel() { Event = e, EventUser = ue };

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            var model = new CreateEventModel { Category = CategoryRepository.Query(s => s.Id != 0).ToList() };
            return View(model);
        }

        [HttpPost]
        [ActionName("createevent")]
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
                    Leader = model.Leader

                });
                
            }
            return RedirectToAction("Events", "Event");
        }

    }
}