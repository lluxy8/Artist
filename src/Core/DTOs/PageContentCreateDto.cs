using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class PageContentCreateDto
    {
        public string Text1 { get; set; } = string.Empty;
        public string Text2 { get; set; } = string.Empty;
        public string Text3 { get; set; } = string.Empty;
        public required IFormFile Image { get; set; }
        public Guid PageId { get; set; }
    }
}
