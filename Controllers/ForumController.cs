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

        [HttpGet]
        public IActionResult ForumView(int? id)
        {
            var forum = ForumRepository.Query(x=> x.Id == id).FirstOrDefault();
            var messages = new List<ForumMessage>();
            if(forum != null){
                messages = ForumMessageRepository.Query(x=> x.ForumId == forum.Id).ToList();
            }
            return View(new Tuple<Forum,List<ForumMessage>>(forum,messages));
        }

        [HttpGet]
        public IActionResult CreateMessage(int id,string message)
        {
            var login = HttpContext.User.Identity.GetLogin();
            var userid = UserRepository.Query(x=> x.Login == login).FirstOrDefault()!.Id;
            ForumMessageRepository.Insert(new ForumMessage()
            {
                UserId = userid,
                ForumId = id,
                Message = message
            });
            return RedirectToAction($"ForumView","forum",new{ id = id});
        }

    }
}