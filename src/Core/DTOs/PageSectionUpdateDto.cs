using Core.Common.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Core.DTOs
{
    public class PageSectionUpdateDto
    {
        public Guid Id { get; set; }

        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 1 en fazla {1} karakter olabilir.")]
        public string? Text1 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 2 en fazla {1} karakter olabilir.")]
        public string? Text2 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Large, ErrorMessage = "Metin 3 en fazla {1} karakter olabilir.")]
        public string? Text3 { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }
        public int Order { get; set; }

        [Required(ErrorMessage = "Sayfa içeriği zorunludur.")]
        public Guid PageContentId { get; set; }
        public bool ImagePosition { get; set; }
    }
}
