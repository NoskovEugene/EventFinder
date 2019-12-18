using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Models.ForumModels
{
    public class CreateForum
    {
        [DisplayName("Тема форума")]
        [Required()]
        public string Theme {get;set;}
    }
}