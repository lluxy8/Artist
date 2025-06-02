using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class SocialCreateDto
    {
        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "Ad en fazla {1} karakter olabilir.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "URL zorunludur.")]
        [StringLength(MaxLenghts.Large, ErrorMessage = "URL en fazla {1} karakter olabilir.")]
        public required string Url { get; set; }
    }
}
