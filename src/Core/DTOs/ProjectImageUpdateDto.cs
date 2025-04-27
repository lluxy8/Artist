using Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class ProjectImageUpdateDto
    {
        [Required(ErrorMessage = "ID zorunludur.")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Resim URL'si zorunludur.")]
        [StringLength(MaxLenghts.XLarge, ErrorMessage = "Resim URL'si en fazla {1} karakter olabilir.")]
        public required string Url { get; set; }
        public bool IsMainImage { get; set; }
        [Required(ErrorMessage = "Proje ID'si zorunludur.")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }
}
