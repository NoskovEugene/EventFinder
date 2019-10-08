using System.ComponentModel.DataAnnotations;

namespace EventFinder.Models.EntitiesAbstraction
{
    public abstract class EntityBase
    {
        [Key]
        public int Id {get;set;}
    }
}