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

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost != null)
            {
                _context.BlogPosts.Remove(existingBlogPost);
                await _context.SaveChangesAsync();
                return existingBlogPost;
            }

            return null;
        }


        // Get all Blog Posts
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(x => x.Categories).ToListAsync();
        }


        // Get Blogpost By Id
        public async Task<BlogPost?> GetBlogPostByIdAsync(Guid id)
        {
            return await _context.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(c => c.Id == id);
        }


        // Get Blogpost By Url Handle
        public async Task<BlogPost> GetBlogPostByUrlHandleAsync(string urlHandle)
        {
            return await _context.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _context.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }

            // Update BlogPost

            _context.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            // Update Categories

            existingBlogPost.Categories = blogPost.Categories;

            await _context.SaveChangesAsync(); 

            return blogPost;
        }
    }
}
