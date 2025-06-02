using CodePluse.API.Models.Domain; // Importing domain models
using Microsoft.EntityFrameworkCore; // Entity Framework Core namespace

namespace CodePluse.API.Data
{
    // This class defines the Entity Framework Core database context
    public class ApplicationDbContext : DbContext
    {
        // Constructor accepts DbContextOptions to configure the context (e.g., connection string, provider)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for BlogPost table — represents a collection of blog posts in the database
        public DbSet<BlogPost> BlogPosts { get; set; }

        // DbSet for Category table — represents a collection of categories in the database
        public DbSet<Category> Categories { get; set; }


        // DbSet for BlogImage table — represents a collection of categories in the database
        public DbSet<BlogImage> BlogImages { get; set; }



    }
}
