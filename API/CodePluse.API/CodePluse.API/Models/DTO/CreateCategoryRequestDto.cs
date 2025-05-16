using System.ComponentModel.DataAnnotations;

namespace CodePluse.API.Models.DTO
{
    public class CreateCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UrlHandle { get; set; }
    }
}
