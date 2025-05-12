using CodePluse.API.Models.Domain;

namespace CodePluse.API.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category?> GetCategoryByIdAsync (Guid id);

        Task<Category?> UpdateAsync(Category category);

        Task<Category?> DeleteAsync(Guid id);

        




    }
}
