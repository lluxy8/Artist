namespace Web.Models
{
    public class DetailViewModel
    {
        public string ProjectName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string? Reference { get; set; }
        public List<string> Images { get; set; } = [];
        public DateTime CreateDate { get; set; }
    }
}
