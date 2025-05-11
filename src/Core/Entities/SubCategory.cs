using Core.Abstract;

namespace Core.Entities
{
    public class SubCategory : BaseEntity
    {
        public required string DisplayName { get; set; }
        public required string UrlName { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public required string ImageUrl { get; set; }
        public ICollection<Project> Projects { get; set; } = [];
    }
}
