using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class ProjectImageCreateDto
    {
        [Required(ErrorMessage = "Resim zorunludur.")]
        public IFormFile Image { get; set; } = null!;
        public bool IsMainImage { get; set; }
        [Required(ErrorMessage = "Proje ID'si zorunludur.")]
        public Guid ProjectId { get; set; }
    }
}
