using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Repository.Interface;

namespace CodePluse.API.Repository.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {

        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            this._context = context;
        }


        // Create a single BlogPost
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();

            return blogPost;
        }
    }
}
