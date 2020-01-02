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

        public EventController(IRepositoryBase<Event> eventRepo,
                               IRepositoryBase<User> userRepo,
                               IRepositoryBase<Category> catRepo)
        {
            this.EventRepository = eventRepo;
            this.UserRepository = userRepo;
            this.CategoryRepository = catRepo;

        }

        [Route("Events/")]
        public IActionResult Events()
        {
            var events = EventRepository.Query(s => s.Id != 0).ToList();

            return View(events);
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