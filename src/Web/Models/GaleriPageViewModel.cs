using Core.Entities;

namespace Web.Models
{
    public class GaleriPageViewModel
    {
        public List<Category> Categories { get; set; }
        public SubCategory SelectedSub { get; set; }
        public Category? Selected { get; set; }
        public List<Project> Projects { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}
