using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class PageCreateDto
    {
        public required string DisplayName { get; set; }
        public required string UrlName { get; set; }
        public required IFormFile Image { get; set; }
    }
}
