using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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


        // Get all Blog Posts
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(x => x.Categories).ToListAsync();
        }
    }
}
