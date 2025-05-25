using Core.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ProjectCreateDto
    {
        [Required(ErrorMessage = "Proje adı zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "Proje adı en fazla {1} karakter olabilir.")]
        public required string Name { get; set; }
        public required string UrlName { get; set; }

        [StringLength(MaxLenghts.XLarge, ErrorMessage = "Proje açıklaması en fazla {1} karakter olabilir.")]
        public required string Description { get; set; }

        [StringLength(MaxLenghts.Small, ErrorMessage = "Proje açıklaması en fazla {1} karakter olabilir.")]
        public string Reference { get; set; } = string.Empty;
        public bool IsHighlighted { get; set; }
        public bool IsVisible { get; set; } 
        public Guid CategoryId { get; set; }
    }
}
