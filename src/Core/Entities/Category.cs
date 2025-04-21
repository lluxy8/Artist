using Core.Abstract;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        public required string UrlName { get; set; }
        public required string DisplayName { get; set; }
        public required string ImageUrl { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsHighlighted { get; set; }
        public List<Project> Projects { get; set; } = [];
    }
}
