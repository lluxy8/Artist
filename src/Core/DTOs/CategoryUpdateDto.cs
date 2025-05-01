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
        [StringLength(MaxLenghts.Small, MinimumLength = 3, ErrorMessage = "URL adı {2} ile {1} karakter arasında olmalıdır.")]
        public required string UrlName { get; set; }

        [Required(ErrorMessage = "Görünen ad zorunludur.")]
        [StringLength(MaxLenghts.Small, MinimumLength = 3, ErrorMessage = "Görünen {2} ile {1} karakter arasında olmalıdır.")]
        public required string DisplayName { get; set; }

        [Required(ErrorMessage = "Resim zorunludur.")]

        [StringLength(MaxLenghts.Large, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsHighlighted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
