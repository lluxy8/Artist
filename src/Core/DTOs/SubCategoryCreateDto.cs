using Core.Common.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SubCategoryCreateDto
    {
        [MaxLength(MaxLenghts.Small, ErrorMessage = "Görünen ad en fazla {1} karakter olabilir.")]
        public required string DisplayName { get; set; }

        [MaxLength(MaxLenghts.Small, ErrorMessage = "Url en fazla {1} karakter olabilir.")]
        public required string UrlName { get; set; }
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Resim zorunludur.")]
        public required IFormFile Image { get; set; }
    }
}
