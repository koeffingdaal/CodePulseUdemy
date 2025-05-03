using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Repository.Interface;

namespace CodePluse.API.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
