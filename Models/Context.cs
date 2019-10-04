using Microsoft.EntityFrameworkCore;

namespace EventFinder.Models
{
    public class Context : DbContext
    {

        public Context(){}

        public Context(DbContextOptions<Context> options):base(options){}

        public DbSet<User> Users {get;set;}
    }
}