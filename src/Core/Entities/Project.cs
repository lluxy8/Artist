using Core.Abstract;

namespace Core.Entities
{
    public class Project : BaseEntity
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsHighlighted { get; set; }
        public bool IsVisible { get; set; } = true;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<ProjectImage> Images { get; set; } = [];
    }
}
