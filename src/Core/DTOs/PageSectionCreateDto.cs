using System.ComponentModel;
using Core.Common.Constants;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class PageSectionCreateDto
    {
        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 1 en fazla {1} karakter olabilir.")]
        public string? Text1 { get; set; }

        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 2 en fazla {1} karakter olabilir.")]
        public string? Text2 { get; set; }

        [StringLength(MaxLenghts.Large, ErrorMessage = "Metin 3 en fazla {1} karakter olabilir.")]
        public string? Text3 { get; set; }
        public IFormFile? Image { get; set; }
        public int Order { get; set; }

        [Required(ErrorMessage = "Sayfa içeriği zorunludur.")]
        public Guid PageContentId { get; set; }
        public bool ImagePosition { get; set; }
    }
}
