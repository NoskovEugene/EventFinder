using System.Security.Cryptography.X509Certificates;
using System;
using EventFinder.Models.EntitiesAbstraction;

namespace EventFinder.Models.Entity
{
    public class Forum : EntityBase
    {
        public DateTime CreationTime {get;set;}
        
        public virtual User Owner {get;set;}

        public int OwnerId{get;set;}

        public string Theme{get;set;}
    }
}