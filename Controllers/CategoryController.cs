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

        [Route("Category/{id}")]
        public IActionResult Category(int id)
        {
            var model = new CategoryViewModel 
            {
                Name = CategoryRepository.Query(s => s.Id == id).Select(s=>s.Name).First(),
                Events = EventRepository.Query(s => s.Id == id).ToList(),
                Forums = ForumRepository.Query(s => s.Id == id).ToList()
            };

            return View(model);
        }
    }
}