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
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "ID zorunludur.")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "URL adı zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "URL adı en fazla {1} karakter olabilir.")]
        public required string UrlName { get; set; }
        [Required(ErrorMessage = "Görünen ad zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "Görünen ad en fazla {1} karakter olabilir.")]
        public required string DisplayName { get; set; }
        [Required(ErrorMessage = "Resim URL'si zorunludur.")]
        [StringLength(MaxLenghts.Medium, ErrorMessage = "Resim URL'si en fazla {1} karakter olabilir.")]
        public required string ImageUrl { get; set; }
        [StringLength(MaxLenghts.Large, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public string Description { get; set; } = string.Empty;
        public bool IsHighlighted { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }
}
