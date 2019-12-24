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
        public DateTime CreationDate{get;set;}

        public DateTime DeleteDate{get;set;}

        public DateTime EventDate{get;set;}

        public string Leader {get;set;}

        public string Place {get;set;}

        public string Description{get;set;}
    }
}