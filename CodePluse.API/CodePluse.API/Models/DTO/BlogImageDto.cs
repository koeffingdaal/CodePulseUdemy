namespace CodePluse.API.Models.DTO
{
    public class BlogImageDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string FileExtenstion { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
    }
}