using CodePluse.API.Models.Domain;

namespace CodePluse.API.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
    }
}
