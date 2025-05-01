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

        [StringLength(MaxLenghts.XLarge, ErrorMessage = "Proje açıklaması en fazla {1} karakter olabilir.")]
        public required string Description { get; set; }
        public bool IsHighlighted { get; set; }
        public bool IsVisible { get; set; }
        public Guid CategoryId { get; set; }
    }
}
