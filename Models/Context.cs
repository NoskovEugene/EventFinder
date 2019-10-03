using Microsoft.EntityFrameworkCore;

namespace EventFinder.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users {get;set;}
    }
}