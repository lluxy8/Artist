using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Required(ErrorMessage = "Sayfa İçeriği ID'si zorunludur.")]
        public Guid PageContentId { get; set; }
    }
}
