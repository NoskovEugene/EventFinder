using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFinder.Models;
using EventFinder.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventFinder
{
    [Route("api/[controller]")]
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
            
            string json = JsonConvert.SerializeObject(context.Event.Select(s=> new { id = s.Id, date = $"{ValueChanger.ChangeNum(s.EventDate.Year)}-{ValueChanger.ChangeNum(s.EventDate.Month)}-{ValueChanger.ChangeNum(s.EventDate.Day)}", time = $"{ValueChanger.ChangeNum(s.EventDate.Hour)}:{ValueChanger.ChangeNum(s.EventDate.Minute)}", title = s.Name, description = s.Description, owner = s.Owner.Login, place = s.Place, category = s.Category.Name }));
            return json;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
