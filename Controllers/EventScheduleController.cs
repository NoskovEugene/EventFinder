using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFinder.Models;
using EventFinder.Models.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventFinder
{
    [Route("api/[controller]")]
    public class EventScheduleController : Controller
    {
        private readonly EventFinderContext _context;
        public EventScheduleController(EventFinderContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Event>> Get()
        {
            var category = new Category { Name = "Dances"};
            _context.Category.Add(category);
            _context.SaveChanges();
            var dance = new Event {  };
            return _context.Event.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
