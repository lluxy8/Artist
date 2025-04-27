using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "URL adı zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "URL adı en fazla {1} karakter olabilir.")]
        public required string UrlName { get; set; }

        [Required(ErrorMessage = "Görünen ad zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "Görünen ad en fazla {1} karakter olabilir.")]
        public required string DisplayName { get; set; }

        [Required(ErrorMessage = "Resim zorunludur.")]
        public IFormFile? Image { get; set; }

        [StringLength(MaxLenghts.Large, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public string Description { get; set; } = string.Empty;

        public bool IsHighlighted { get; set; }
    }
}
