// Required namespaces for project structure and ASP.NET Core functionality
using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Models.DTO;
using CodePluse.API.Repository.Implementation;
using CodePluse.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
{
    // Base route for the controller, e.g., https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController] // Enables automatic model validation and binding
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        // Constructor with dependency injection of repository
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        // POST: Create a single category
        // Route: https://localhost:xxxx/api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto requestDto)
        {
            // Convert DTO to domain model
            var category = new Category
            {
                Name = requestDto.Name,
                UrlHandle = requestDto.UrlHandle
            };

            // Async call should use 'await' instead of '.Wait()' to avoid deadlocks
            await _categoryRepository.CreateAsync(category);

            // Map domain model back to DTO for response
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response); // Returns 200 OK with created category
        }

        // GET: Get all categories
        // Route: https://localhost:xxxx/api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            // Map domain models to DTOs
            var response = new List<CategoryDto>();

            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response); // Returns list of categories
        }

        // GET: Get category by ID
        // Route: https://localhost:xxxx/api/categories/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound(); // Returns 404 if category doesn't exist
            }

            // Map domain model to DTO
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };

            return Ok(response);
        }

        // PUT: Edit category by ID
        // Route: https://localhost:xxxx/api/categories/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, EditCategoryRequestDto requestDto)
        {
            // Convert DTO to domain model
            var category = new Category
            {
                Id = id,
                Name = requestDto.Name,
                UrlHandle = requestDto.UrlHandle
            };

            // Try to update the category
            category = await _categoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound(); // Return 404 if update failed (e.g., category not found)
            }

            // Convert updated domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response); // Return 200 OK with updated category
        }



        // DELETE: Delete category by ID
        // Route: https://localhost:xxxx/api/categories/{id}

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryRepository.DeleteAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            // Map Domain model to DTO

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        
    }
}
