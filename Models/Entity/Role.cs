using EventFinder.Models.EntitiesAbstraction;
using System.Collections.Generic;
namespace EventFinder.Models.Entity    
{
    public class Role : EntityBase
    {
        public string RoleName {get;set;}

        public virtual IList<UserRole> UserRoles {get;set;}
    }
}