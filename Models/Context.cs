using Microsoft.EntityFrameworkCore;

namespace EventFinder.Models
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options):base(options){}

        public DbSet<User> User {get;set;}

        public DbSet<Role> Role {get;set;}

        
    }
}