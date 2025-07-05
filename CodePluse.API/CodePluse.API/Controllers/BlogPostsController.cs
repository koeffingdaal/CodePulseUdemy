using CodePluse.API.Models.Domain;
using CodePluse.API.Models.DTO;
using CodePluse.API.Repository.Implementation;
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

        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
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
                Categories = new List<Category>()

            };

            // Checking if categories exist in db or not

            foreach (var categoryGuid in dto.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryByIdAsync(categoryGuid);

                if (existingCategory != null)
                {
                    blogpost.Categories.Add(existingCategory);

                }
            }


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
                Categories = blogpost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle

                }).ToList()


            };

            return Ok(response);


        }


        [HttpGet]

        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogpost = await blogPostRepository.GetAllAsync();

            // Convert Domain to DTO

            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogpost)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var existingBlogPost = await blogPostRepository.GetBlogPostByIdAsync(id);

            if (existingBlogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = existingBlogPost.Id,
                Author = existingBlogPost.Author,
                Content = existingBlogPost.Content,
                FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
                IsVisible = existingBlogPost.IsVisible,
                PublishedDate = existingBlogPost.PublishedDate,
                ShortDescription = existingBlogPost.ShortDescription,
                Title = existingBlogPost.Title,
                UrlHandle = existingBlogPost.UrlHandle,
                Categories = existingBlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };



            return Ok(response);


        }

        [HttpGet]
        [Route("{urlHandle}")]

        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            // Get Blogpost Details from Repository

            var blogPostDetails = await blogPostRepository.GetBlogPostByUrlHandleAsync(urlHandle);

            // Convert Domain model to DTO

            if (blogPostDetails == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPostDetails.Id,
                Author = blogPostDetails.Author,
                Content = blogPostDetails.Content,
                FeaturedImageUrl = blogPostDetails.FeaturedImageUrl,
                IsVisible = blogPostDetails.IsVisible,
                PublishedDate = blogPostDetails.PublishedDate,
                ShortDescription = blogPostDetails.ShortDescription,
                Title = blogPostDetails.Title,
                UrlHandle = blogPostDetails.UrlHandle,
                Categories = blogPostDetails.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };



            return Ok(response);

        }


        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateBlogPosyById([FromRoute] Guid id, UpdateBlogPostRequestDto updateBlogPostRequestDto)
        {
            // Convert DTO to Domain Model

            var blogPost = new BlogPost
            {
                Id = id,
                Author = updateBlogPostRequestDto.Author,
                Content = updateBlogPostRequestDto.Content,
                FeaturedImageUrl = updateBlogPostRequestDto.FeaturedImageUrl,
                IsVisible = updateBlogPostRequestDto.IsVisible,
                PublishedDate = updateBlogPostRequestDto.PublishedDate,
                ShortDescription = updateBlogPostRequestDto.ShortDescription,
                Title = updateBlogPostRequestDto.Title,
                UrlHandle = updateBlogPostRequestDto.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in updateBlogPostRequestDto.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryByIdAsync(categoryGuid);

                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            // Call Repository to update Domain Model

            var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

            if (updatedBlogPost == null)
            {
                return null;
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            
            return Ok(response);


        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var deletedBlogPost = await blogPostRepository.DeleteAsync(id);

            if (deletedBlogPost == null)
            {
                return null;
            }

            // Convert DTO model

            var response = new BlogPostDto
            {
                Id = deletedBlogPost.Id,
                Title = deletedBlogPost.Title,
                ShortDescription= deletedBlogPost.ShortDescription,
                PublishedDate = deletedBlogPost.PublishedDate,
                FeaturedImageUrl= deletedBlogPost.FeaturedImageUrl,
                Author = deletedBlogPost.Author,
                Content = deletedBlogPost.Content,
                UrlHandle= deletedBlogPost.UrlHandle,
                IsVisible = deletedBlogPost.IsVisible

            };

            return Ok(response);
        }


    }
}
