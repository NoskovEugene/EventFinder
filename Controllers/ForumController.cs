using System.Collections.Generic;
using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventFinder.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EventFinder.Models.ForumModels;
using EventFinder.Models.Repositories;
using EventFinder.Models.Entity;
using EventFinder.Extensions;
namespace EventFinder.Controllers
{
    [Authorize()]
    [Route("forum/[action]")]
    public class ForumController : Controller
    {
        
        protected IRepositoryBase<Forum> ForumRepository{get;set;}

        protected IRepositoryBase<ForumMessage> ForumMessageRepository {get;set;}

        protected IRepositoryBase<User> UserRepository{get;set;}

        public ForumController(IRepositoryBase<Forum> forumRepo,
                               IRepositoryBase<ForumMessage> forumMessageRepo,
                               IRepositoryBase<User> userRepo)
        {
            this.ForumRepository = forumRepo;
            this.ForumMessageRepository = forumMessageRepo;
            this.UserRepository = userRepo;
        }

        public IActionResult Forums()
        {
            var forums = ForumRepository.Query(x=> x.Id != 0).ToList();
            return View(forums);
        }

        [HttpGet]
        public IActionResult CreateForum()
        {
            return View();
        }

        [HttpPost]
        [ActionName("createforum")]
        public IActionResult CreateForum(CreateForum model)
        {
            if(ModelState.IsValid)
            {
                var login = HttpContext.User.Identity.GetLogin();
                var user = UserRepository.Query(x=> x.Login == login).FirstOrDefault();
                ForumRepository.Insert(new Forum() {
                    CreationTime = DateTime.Now,
                    OwnerId = user.Id,
                    Theme = model.Theme
                });
            }
            return Ok();
        }

    }
}