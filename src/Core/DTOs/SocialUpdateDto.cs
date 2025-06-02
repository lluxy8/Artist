using Core.Common.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class SocialUpdateDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(MaxLenghts.Small, ErrorMessage = "Ad en fazla {1} karakter olabilir.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "URL zorunludur.")]
        [StringLength(MaxLenghts.Large, ErrorMessage = "URL en fazla {1} karakter olabilir.")]
        public required string Url { get; set; }
        public required string IconUrl { get; set; }
    }
}
