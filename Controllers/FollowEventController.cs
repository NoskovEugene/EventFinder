using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFinder.Extensions;
using EventFinder.Models;
using EventFinder.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowEventController : ControllerBase
    {
        private readonly EventFinderContext context;
        public FollowEventController(EventFinderContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public string Get(int id)
        {
            var login = HttpContext.User.Identity.GetLogin();
            var idUser = context.User.Where(x => x.Login == login).Select(s => s.Id).First();
            var eu = new EventUser() { EventId = id, UserId = idUser };

            if (context.EventUser.Where(s => s.EventId == id && s.UserId == idUser).Count() == 0)
            {
                context.EventUser.Add(eu);
            }
            else
            {
                context.EventUser.Remove(eu);
            }
            context.SaveChanges();

            var eventUsers = JsonConvert.SerializeObject(context.EventUser.Where(s => s.EventId == id && s.UserId == idUser));

            return eventUsers;
        }
    }
}