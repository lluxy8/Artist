using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Core.Common.Constants;

namespace Core.DTOs
{
    public class SettingUpdateDto
    {
        [Required(ErrorMessage = "ID zorunludur.")]
        public Guid Id { get; set; }

        public IFormFile? Image { get; set; }

        [StringLength(MaxLenghts.XLarge, ErrorMessage = "Logo URL'si en fazla {1} karakter olabilir.")]
        public string LogoUrl { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Small, ErrorMessage = "Şirket adı en fazla {1} karakter olabilir.")]
        public string CompanyName { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Large, ErrorMessage = "Adres en fazla {1} karakter olabilir.")]
        public string Address { get; set; } = string.Empty;

        [StringLength(MaxLenghts.XLarge, ErrorMessage = "Google Maps adresi en fazla {1} karakter olabilir.")]
        public string AddressGoogleMaps { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Small, ErrorMessage = "Telefon numarası en fazla {1} karakter olabilir.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Small, ErrorMessage = "E-posta en fazla {1} karakter olabilir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Small, ErrorMessage = "Tamamlanan proje sayısı en fazla {1} karakter olabilir.")]
        public string DoneProjectsCount { get; set; } = string.Empty;

        [StringLength(MaxLenghts.Small, ErrorMessage = "Müşteri sayısı en fazla {1} karakter olabilir.")]
        public string DoneCustomerCount { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "Deneyim yılı 0 ile 100 arasında olmalıdır.")]
        public int ExperienceYear { get; set; }
    }
}
