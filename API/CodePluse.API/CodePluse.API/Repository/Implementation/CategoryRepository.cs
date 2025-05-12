using CodePluse.API.Data; // Access to database context
using CodePluse.API.Models.Domain; // Access to domain models
using CodePluse.API.Repository.Interface; // Access to the repository interface
using Microsoft.EntityFrameworkCore; // Entity Framework Core functionality

namespace CodePluse.API.Repository.Implementation
{
    // Implementation of ICategoryRepository for handling Category data operations
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor injection of ApplicationDbContext
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }

        // Create and save a new category to the database
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.AddAsync(category); // Add the category
            await _context.SaveChangesAsync(); // Persist changes to the database
            return category;
        }






        // Delete a single Category
        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory is null)
            {
                return null;
            }

            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;
        }



        // Retrieve all categories
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync(); // Returns all categories as a list
        }

        // Retrieve a single category by its ID
        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id); // Null if not found
        }

        // Update an existing category
        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (existingCategory != null)
            {
                // Set the current values of the existing entity to the new ones
                _context.Entry(existingCategory).CurrentValues.SetValues(category);

                await _context.SaveChangesAsync(); // ✅ Awaiting is important here
                return category;
            }

            return null; // Return null if no matching category was found
        }
    }
}
