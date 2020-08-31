using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventFinder.Models.EntitiesAbstraction;

namespace EventFinder.Models.Entity
{
    public class Event : EntityBase
    {
        public virtual User Owner { get; set; }
        [ForeignKey("UserId")]
        public int OwnerId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate{get;set;}

        [DataType(DataType.Date)]
        public DateTime EventDate{get;set;}

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Place {get;set;}

        [DataType(DataType.MultilineText)]
        public string Description{get;set;}

        [RegularExpression(@"/^(https?:\/\/)?([\w\.]+)\.([a-z]{2,6}\.?)(\/[\w\.]*)*\/?$/")]
        public string EventLink { get; set; }

        public virtual List<Forum> Forums { get; set; }

        public virtual IList<EventUser> EventUsers { get; set; }
    }
}