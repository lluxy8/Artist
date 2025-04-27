using Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class PageContentUpdateDto
    {
        [Required(ErrorMessage = "ID zorunludur.")]
        public Guid Id { get; set; }

        public IFormFile? Image { get; set; }

        [StringLength(MaxLenghts.Large, ErrorMessage = "Metin 1 en fazla {1} karakter olabilir.")]
        public string Text1 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Large, ErrorMessage = "Metin 2 en fazla {1} karakter olabilir.")]
        public string Text2 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Large, ErrorMessage = "Metin 3 en fazla {1} karakter olabilir.")]
        public string Text3 { get; set; } = string.Empty;

        [StringLength(MaxLenghts.XLarge, ErrorMessage = "Resim URL'si en fazla {1} karakter olabilir.")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sayfa ID'si zorunludur.")]
        public Guid PageId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
