using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using EventFinder.Models.Entity;
using System.Collections.Generic;

namespace EventFinder.Models.ForumModels
{
    public class CreateForum
    {
        [DisplayName("Тема форума")]
        [Required()]
        public string Theme {get;set;}
        public List<Category> Category { get; set; }

        public int? CategoryId { get; set; }

        public List<Event> Event { get; set; }

        public int? EventId { get; set; }
    }
}