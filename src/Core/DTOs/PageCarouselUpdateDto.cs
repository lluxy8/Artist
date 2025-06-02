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
    public class PageCarouselUpdateDto
    {
        [Required(ErrorMessage = "ID zorunludur.")]
        public Guid Id { get; set; }
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Resim URL'si zorunludur.")]
        [StringLength(MaxLenghts.XLarge, ErrorMessage = "Resim URL'si en fazla {1} karakter olabilir.")]
        public required string ImageUrl { get; set; } 
        [Required(ErrorMessage = "Metin 1 zorunludur.")]
        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 1 en fazla {1} karakter olabilir.")]
        public required string Text1 { get; set; } 
        [Required(ErrorMessage = "Metin 2 zorunludur.")]
        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 2 en fazla {1} karakter olabilir.")]
        public required string Text2 { get; set; }
        [Required(ErrorMessage = "Metin 3 zorunludur.")]
        [StringLength(MaxLenghts.Medium, ErrorMessage = "Metin 3 en fazla {1} karakter olabilir.")]
        public required string Text3 { get; set; } 
        [Required(ErrorMessage = "Sayfa İçeriği ID'si zorunludur.")]
        public Guid PageContentId { get; set; }
        [Required(ErrorMessage = "Sayfa İçeriği zorunludur.")]
        public int DisplayOrder { get; set; }
        public required PageContent PageContent { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
