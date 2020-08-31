using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFinder.Extensions;
using EventFinder.Models;
using EventFinder.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventFinder
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventScheduleController : Controller
    {
        private readonly EventFinderContext context;
        public EventScheduleController(EventFinderContext context)
        {
            this.context = context;
        }

        // GET api/<controller>/5
        [HttpGet]
        public string Get()
        {
            string json = JsonConvert.SerializeObject(context.Event.Select(s=> new { id = s.Id, date = $"{s.EventDate.Year.ChangeNum()}-{s.EventDate.Month.ChangeNum()}-{s.EventDate.Day.ChangeNum()}", time = $"{s.EventDate.Hour.ChangeNum()}:{s.EventDate.Minute.ChangeNum()}", title = s.Name, description = s.Description.ChangeString(), owner = s.Owner.Login, place = s.Place, category = s.Category.Name }));
            return json;
        }

    }
}
