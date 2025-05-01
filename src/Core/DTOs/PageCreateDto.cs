using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class PageCreateDto
    {
        [Required(ErrorMessage = "Görünen ad zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "Görünen ad en fazla {1} karakter olabilir.")]
        public required string DisplayName { get; set; }

        [Required(ErrorMessage = "URL adı zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "URL adı en fazla {1} karakter olabilir.")]
        public required string UrlName { get; set; }

        public bool ListProjects { get; set; }
        public bool ListCategories { get; set; }
    }
}
