using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class ProjectImageCreateDto
    {
        public IFormFile Image { get; set; } = null!;
        public bool IsMainImage { get; set; }
        public Guid ProjectId { get; set; }
    }
}
