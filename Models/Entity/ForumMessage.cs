using EventFinder.Models.EntitiesAbstraction;

namespace EventFinder.Models.Entity
{
    public class ForumMessage : EntityBase
    {
        public int UserId {get;set;}
        
        public virtual User User {get;set;}

        public virtual Forum Forum {get;set;}

        public int ForumId {get;set;}

        public string Message {get;set;}
    }
}