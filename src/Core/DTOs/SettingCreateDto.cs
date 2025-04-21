using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SettingCreateDto
    {
        public required IFormFile Image { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AddressGoogleMaps { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DoneProjectsCount { get; set; } = string.Empty;
        public string DoneCustomerCount { get; set; } = string.Empty;
        public int ExperienceYear { get; set; }
    }
}
