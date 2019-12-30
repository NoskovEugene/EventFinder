using System;
using System.ComponentModel.DataAnnotations;
using EventFinder.Models.EntitiesAbstraction;

namespace EventFinder.Models.Entity
{
    public class Event : EntityBase
    {
        public virtual User Owner { get; set; }

        public int OwnerId { get; set; }

        public string Name { get; set; }

        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreationDate{get;set;}

        public DateTime DeleteDate{get;set;}

        public DateTime EventDate{get;set;}

        public DateTime EventDateStart { get; set; }

        public DateTime EventDateEnd { get; set; }

        public string Leader {get;set;}

        public string Place {get;set;}

        public string Description{get;set;}
    }
}