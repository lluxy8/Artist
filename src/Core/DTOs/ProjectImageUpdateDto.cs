using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class ProjectImageUpdateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public IFormFile? Image { get; set; }
        public required string Url { get; set; }
        public bool IsMainImage { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }
}
