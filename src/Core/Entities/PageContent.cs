using Core.Abstract;

namespace Core.Entities
{
    public class PageContent : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public List<PageSection> PageSections { get; set; } = [];
        public List<PageCarousel> PageCarousels { get; set; } = [];
        public Guid PageId { get; set; }
        public Page Page { get; set; } = null!;

    }
}
