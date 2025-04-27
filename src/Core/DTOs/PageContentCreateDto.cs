using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class PageContentCreateDto
    {
        [StringLength(MaxLenghts.Large, ErrorMessage = "Metin 1 en fazla {1} karakter olabilir.")]
        public string Text1 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Large, ErrorMessage = "Metin 2 en fazla {1} karakter olabilir.")]
        public string Text2 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Large, ErrorMessage = "Metin 3 en fazla {1} karakter olabilir.")]
        public string Text3 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Resim zorunludur.")]
        public required IFormFile Image { get; set; }

        [Required(ErrorMessage = "Sayfa ID'si zorunludur.")]
        public Guid PageId { get; set; }
    }
}
