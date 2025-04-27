using Core.Entities;

namespace Web.Models
{
    public class FooterViewModel
    {
        public Setting Settings { get; set; }
        public List<Social> Socials { get; set; }
        public List<Category> Categories { get; set; }
        public List<Project> Projects { get; set; }
    }
}
