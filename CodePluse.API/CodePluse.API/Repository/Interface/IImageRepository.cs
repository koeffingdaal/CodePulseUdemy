using CodePluse.API.Models.Domain;

namespace CodePluse.API.Repository.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload (IFormFile file, BlogImage blogImage);
    }
}
