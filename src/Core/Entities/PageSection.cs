using Core.Abstract;

namespace Core.Entities
{
    public class PageSection : BaseEntity
    {
        public required string Text1 { get; set; } 
        public required string Text2 { get; set; } 
        public required string Text3 { get; set; } 
        public required string ImageUrl { get; set; }
        public bool ImagePosition { get; set; } // True sağ
        public int DisplayOrder { get; set; }
        public required Guid PageContentId { get; set; }
        public PageContent PageContent { get; set; } = null!;
    }
}
