using Core.Abstract;

namespace Core.Entities
{
    public class Setting : BaseEntity
    {
        public string LogoUrl { get; set; } = string.Empty;
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
