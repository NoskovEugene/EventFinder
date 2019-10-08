using Microsoft.EntityFrameworkCore;
using EventFinder.Models.Entity;
namespace EventFinder.Models
{
    public class EventFinderContext : DbContext
    {

        public EventFinderContext(DbContextOptions<EventFinderContext> options):base(options){}

        public DbSet<User> User {get;set;}

        public DbSet<Role> Role {get;set;}
    }
}