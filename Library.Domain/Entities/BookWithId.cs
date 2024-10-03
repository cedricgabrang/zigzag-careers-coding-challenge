using Swashbuckle.AspNetCore.Annotations;

namespace Library.Domain.Entities
{
    public class BookWithId
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }

        [SwaggerSchema(Format = "date")]
        public string? PublishedDate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");
    }
}
