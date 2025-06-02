using Core.Entities;

namespace Web.Models
{
    public class PageViewModel
    {
        public required string Url { get; set; }
        public required List<PageSection> PageSections { get; set; }
        public List<Project> Projects { get; set; } = [];
        public List<Category> Categories { get; set; } = [];
    }
}
