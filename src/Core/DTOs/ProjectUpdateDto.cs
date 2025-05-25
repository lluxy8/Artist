using Core.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ProjectUpdateDto
    {
        public Guid Id { get; set; }

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
        public Guid SelectedCategoryId { get; set; }
        public Guid SelectedSubCategoryId { get; set; }
    }
}
