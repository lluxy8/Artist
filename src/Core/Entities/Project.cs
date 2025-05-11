using Core.Abstract;

namespace Core.Entities
{
    public class Project : BaseEntity
    {
        public required string DisplayName { get; set; }
        public required string UrlName { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsHighlighted { get; set; }
        public bool IsVisible { get; set; } = true;
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; } = null!;
        public List<ProjectImage> Images { get; set; } = [];
    }
}
