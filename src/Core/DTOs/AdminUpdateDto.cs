using Core.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class AdminUpdateDto
    {
        public Guid Id { get; set; }
        public string IpAdress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [StringLength(MaxLenghts.Small, MinimumLength = 5, ErrorMessage = "Kullanıcı adı {2} ile {1} karakter arasında olmalıdır.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [StringLength(MaxLenghts.Small, MinimumLength = 6, ErrorMessage = "Şifre {2} ile {1} karakter arasında olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
            ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.")]
        public required string Password { get; set; }
    }
}
