using Microsoft.EntityFrameworkCore;
using EventFinder.Models.Entity;

namespace EventFinder.Models
{
    public class EventFinderContext : DbContext
    {

        public EventFinderContext(DbContextOptions<EventFinderContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasKey(table=> new { table.UserId, table.RoleId} );
        }

        public DbSet<User> User {get;set;}

        public DbSet<Role> Role {get;set;}
    }
}