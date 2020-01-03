using EventFinder.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventFinder.Models.EventModels
{
    public class EventDetailsViewModel
    {
        public Event Event { get; set; }
        public EventUser EventUser { get; set; }
    }
}
