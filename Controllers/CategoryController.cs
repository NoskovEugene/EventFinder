using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFinder.Models.CategoryModels;
using EventFinder.Models.Entity;
using EventFinder.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventFinder.Controllers
{
    public class CategoryController : Controller
    {
        protected IRepositoryBase<Event> EventRepository { get; set; }
        protected IRepositoryBase<Category> CategoryRepository { get; set; }

        protected IRepositoryBase<Forum> ForumRepository { get; set; }

        public CategoryController(IRepositoryBase<Forum> forumRepo, 
                                IRepositoryBase<Event> eventRepo,
                               IRepositoryBase<Category> catRepo)
        {
            this.EventRepository = eventRepo;
            this.CategoryRepository = catRepo;
            this.ForumRepository = forumRepo;

        }

        [Route("category/{id}")]
        public IActionResult Category(int id)
        {
            var model = CategoryRepository.Query(s => s.Id == id).First();

            return View(model);
        }

        [Route("category/{id}/events")]
        public IActionResult EventsByCategory (int id)
        {
            var events = EventRepository.Query(s => s.CategoryId == id).ToList();

            return PartialView("Events", events);
        }

        [Route("category/{id}/forums")]
        public IActionResult ForumsByCategory(int id)
        {
            var forums = ForumRepository.Query(s => s.CategoryId == id).ToList();

            return PartialView("Forums", forums);
        }
    }
}