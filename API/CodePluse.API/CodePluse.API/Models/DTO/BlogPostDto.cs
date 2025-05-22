using System.ComponentModel.DataAnnotations;

namespace CodePluse.API.Models.DTO
{
    public class BlogPostDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string FeaturedImageUrl { get; set; }

        [Required]
        public string UrlHandle { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        [Required]
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
