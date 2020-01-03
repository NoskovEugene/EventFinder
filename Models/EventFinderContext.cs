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
            builder.Entity<User>().HasIndex(u=> new { u.Login}).IsUnique(true);
            builder.Entity<UserRole>().HasKey(table=> new { table.UserId, table.RoleId} );
            builder.Entity<Category>().HasIndex(c => new { c.Name }).IsUnique(true);
            builder.Entity<EventUser>().HasKey(table => new { table.EventId, table.UserId });
        }

        public DbSet<User> User {get;set;}

        public DbSet<Role> Role {get;set;}

        public DbSet<Event> Event {get;set;}
        public DbSet<EventUser> EventUser { get; set; }

        public DbSet<ForumMessage> ForumMessage {get;set;}

        public DbSet<Forum> Forum {get;set; }

        public DbSet<Category> Category { get; set; }
    }
}