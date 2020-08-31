using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using EventFinder.Models.Entity;
using System.Collections.Generic;

namespace EventFinder.Models.ForumModels
{
    public class CreateForum
    {

        [Required()]
        [DisplayName("Тема")]
        public string Theme {get;set;}
        public List<Category> Category { get; set; }
        [Required(ErrorMessage = "Поле {0} должно быть заполнено")]
        [DisplayName("Категория")]
        public int CategoryId { get; set; }

        public List<Event> Event { get; set; }

        
        [DisplayName("Мероприятие")]
        public int? EventId { get; set; }
    }
}