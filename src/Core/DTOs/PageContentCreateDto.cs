using Core.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class PageContentCreateDto
    {
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "Başlık en fazla {1} karakter olabilir.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(MaxLenghts.Medium, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Sayfa zorunludur.")]
        public Guid PageId { get; set; }
    }
}
