using CodePluse.API.Models.Domain;
using CodePluse.API.Models.DTO;
using CodePluse.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostController(IBlogPostRepository repository)
        {
            this.blogPostRepository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto createBlogPostRequestDto)
        {
            var blogPost = new BlogPost
            {
                Title = createBlogPostRequestDto.Title,
                ShortDescription = createBlogPostRequestDto.ShortDescription,
                Content = createBlogPostRequestDto.Content,
                FeaturedImageUrl = createBlogPostRequestDto.FeaturedImageUrl,
                UrlHandle = createBlogPostRequestDto.UrlHandle,
                PublishedDate = createBlogPostRequestDto.PublishedDate,
                Author = createBlogPostRequestDto.Author,
                IsVisible = createBlogPostRequestDto.IsVisible
            };

            blogPost = await blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Content = blogPost.Content
            };

            return Ok(response); // ✅ Wrap response
        }
    }
}
