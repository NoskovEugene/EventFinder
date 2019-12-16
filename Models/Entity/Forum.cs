using System;
using EventFinder.Models.EntitiesAbstraction;

namespace EventFinder.Models.Entity
{
    public class Forum : EntityBase
    {
        public DateTime CreationTime {get;set;}
        
        public virtual User Owner {get;set;}

        public int OwnerId{get;set;}
    }
}