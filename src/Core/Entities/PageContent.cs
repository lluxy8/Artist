using Core.Abstract;

namespace Core.Entities
{
    public class PageContent : BaseEntity
    {
        public string Text1 { get; set; } = string.Empty;
        public string Text2 { get; set; } = string.Empty;
        public string Text3 { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<PageCarousel> PageCarousels { get; set; } = [];
        public Guid PageId { get; set; }
        public Page Page { get; set; } = null!;

    }
}
