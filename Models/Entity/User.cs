using EventFinder.Models.EntitiesAbstraction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace EventFinder.Models.Entity
{
    public class User : EntityBase
    {

        public string Login {get;set;}

        public string Password {get;set;}

        public string Name {get;set;}

        public string SurName {get;set;}

        public string Email {get;set;}

        public string AboutMe {get;set;}

        public virtual Image Image {get;set;}

        public int? ImageId { get; set; }

        public virtual IList<UserRole> UserRoles {get;set;}

        public virtual IList<EventUser> EventUsers { get; set; }

    }
}