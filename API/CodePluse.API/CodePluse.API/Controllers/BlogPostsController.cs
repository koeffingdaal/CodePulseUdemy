using CodePluse.API.Models.Domain;
using CodePluse.API.Models.DTO;
using CodePluse.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {

        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;   
        }

        [HttpPost]

        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto dto)
        {
            // Convert DTO to Domain

            var blogpost = new BlogPost
            {
               
                Author = dto.Author,
                Content = dto.Content,
                FeaturedImageUrl = dto.FeaturedImageUrl,
                IsVisible = dto.IsVisible,
                PublishedDate = dto.PublishedDate,
                ShortDescription = dto.ShortDescription,
                Title = dto.Title,
                UrlHandle = dto.UrlHandle,
            };

            blogpost = await blogPostRepository.CreateAsync(blogpost);

            //Convert Domain to DTO

            var response = new BlogPostDto
            {
                Id = blogpost.Id,
                Author = blogpost.Author,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                IsVisible = blogpost.IsVisible,
                PublishedDate = blogpost.PublishedDate,
                ShortDescription = blogpost.ShortDescription,
                Title = blogpost.Title,
                UrlHandle = blogpost.UrlHandle,

            };

            return Ok(response);


        }
    }
}
