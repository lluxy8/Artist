using Core.Entities;

namespace Web.Models
{
    public class PageViewModel
    {
        public required Page Page { get; set; }
        public List<Project> Projects { get; set; } = [];
        public List<Category> Categories { get; set; } = [];
    }
}
