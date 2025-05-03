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


        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto requestDto)
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

    }
}
 