using CodePluse.API.Models.Domain;

namespace CodePluse.API.Repository.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
    }
}
