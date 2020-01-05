using EventFinder.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Models.EventModels
{
    public class CreateEventModel
    {
        [Required]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public List<Category> Category { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime? EventDateStart { get; set; }

        public DateTime? EventDateEnd { get; set; }

        public string Leader { get; set; }

        [Required]
        public string Place { get; set; }

        public string Description { get; set; }
        
        public string CreateChat { get; set; }

        public string EventLink { get; set; }

    }
}
