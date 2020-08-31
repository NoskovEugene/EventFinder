using EventFinder.Models.EntitiesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventFinder.Models.Entity
{
    public class Category : EntityBase
    {
        public string Name { get; set; }

        public virtual List<Event> Events { get; set; }

        public virtual List<Forum> Forums { get; set; }
    }
}
