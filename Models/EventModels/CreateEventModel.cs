using EventFinder.Models.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Models.EventModels
{
    public class CreateEventModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public List<Category> Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(4)]
        public string Place { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [RegularExpression(@"/^(https?:\/\/)?([\w\.]+)\.([a-z]{2,6}\.?)(\/[\w\.]*)*\/?$/")]
        public string EventLink { get; set; }

        public bool CreateChat { get; set; }

    }
}
