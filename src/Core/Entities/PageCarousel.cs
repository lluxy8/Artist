using Core.Abstract;

namespace Core.Entities
{
    public class PageCarousel : BaseEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string Text1 { get; set; } = string.Empty;
        public string Text2 { get; set; } = string.Empty;
        public string Text3 { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public Guid PageContentId { get; set; }
        public PageContent PageContent { get; set; } = null!;

    }
}
