using Swashbuckle.AspNetCore.Annotations;

namespace Library.Shared.DTOs
{
    public class Book
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }

        [SwaggerSchema(Format = "date")]
        public string? PublishedDate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");
    }
}
