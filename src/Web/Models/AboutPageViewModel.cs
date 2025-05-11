using Core.Entities;

namespace Web.Models
{
    public class AboutPageViewModel
    {
        public Page Page { get; set; }
        public string DoneProjectsCount { get; set; } 
        public string DoneCustomerCount { get; set; }
        public int ExperienceYear { get; set; }
    }
}
