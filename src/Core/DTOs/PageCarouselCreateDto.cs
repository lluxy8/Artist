using Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class PageCarouselCreateDto
    {
        public IFormFile? Image { get; set; }

        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 1 en fazla {1} karakter olabilir.")]
        public string Text1 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 2 en fazla {1} karakter olabilir.")]
        public string Text2 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 3 en fazla {1} karakter olabilir.")]
        public string Text3 { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        [Required(ErrorMessage = "Sayfa İçeriği zorunludur.")]
        public Guid PageContentId { get; set; }
    }
}
