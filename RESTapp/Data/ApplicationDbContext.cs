using Microsoft.EntityFrameworkCore;
using RESTapp.Models;

namespace RESTapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Course> Courses { get; set; }

    }
}
