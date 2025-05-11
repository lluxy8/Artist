using Core.Common.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class SubCategoryUpdateDto
    {
        public Guid Id { get; set; }

        [MaxLength(MaxLenghts.Small, ErrorMessage = "Görünen ad en fazla {1} karakter olabilir.")]
        public required string DisplayName { get; set; }

        [MaxLength(MaxLenghts.Small, ErrorMessage = "Url en fazla {1} karakter olabilir.")]
        public required string UrlName { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
