using EventFinder.Models.EntitiesAbstraction;
using System.Collections.Generic;

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

        public string PathToPhoto {get;set;}
        
        public virtual IList<UserRole> UserRoles {get;set;}
    }
}