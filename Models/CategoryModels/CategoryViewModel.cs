using EventFinder.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventFinder.Models.CategoryModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public List<Event> Events { get; set; }

        public List<Forum> Forums { get; set; } 
    }
}
