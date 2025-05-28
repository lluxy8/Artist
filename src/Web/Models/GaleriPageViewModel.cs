using Core.Entities;

namespace Web.Models
{
    public class GaleriPageViewModel
    {
        public Category? Selected { get; set; }
        public SubCategory? SelectedSub { get; set; }

        public List<Category> Categories { get; set; } = [];
        public List<Project> Projects { get; set; } = [];
    }
}
