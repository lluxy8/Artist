using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class CategoryCreateDto
    {
        public required string UrlName { get; set; }
        public required string DisplayName { get; set; }
        public IFormFile? Image { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsHighlighted { get; set; }
    }
}
