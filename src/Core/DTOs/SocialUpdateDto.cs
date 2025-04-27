using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class SocialUpdateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string IconUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
