using Core.Entities;

namespace Web.Models
{
    public class IletisimPageViewModel
    {

        public string Address { get; set; } = string.Empty;
        public required List<PageSection> PageSections { get; set; }
        public string AddressGoogleMaps { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
