using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Models.DTO;
using CodePluse.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
{
    //https:localhost:xxxx/api/categories

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;  
        }



        // Create a single Category
        [HttpPost]

        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto requestDto)
        {
            //Map DTO to Domain Model

            var category = new Category
            {

                Name = requestDto.Name,
                UrlHandle = requestDto.UrlHandle
            };

            _categoryRepository.CreateAsync(category).Wait();

            // Domain Model to DTO

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        // Get All category

        [HttpGet]

        public async Task<IActionResult> GetAllCategories ()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            // Map to DTOs

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

            return Ok(response);
        }


        //Get Category by ID 

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound();
            }
            // map to DTO

            var response = new CategoryDto { Id = existingCategory.Id, Name = existingCategory.Name, UrlHandle = existingCategory.UrlHandle };

            return Ok(response);
        }



    }
}
 