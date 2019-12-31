using EventFinder.Models.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventFinder.Models.EventModels
{
    public class CreateEventModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<Category> Category { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime? EventDateStart { get; set; }

        public DateTime? EventDateEnd { get; set; }

        public string Leader { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
