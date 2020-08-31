using System.Security.Cryptography.X509Certificates;
using System;
using EventFinder.Models.EntitiesAbstraction;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Models.Entity
{
    public class Forum : EntityBase
    {
        public DateTime CreationTime {get;set;}
        
        public virtual User Owner {get;set;}

        public int OwnerId{get;set;}

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Theme{get;set;}

        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }

        public virtual Event Event { get; set; }

        public int? EventId { get; set; }
    }
}