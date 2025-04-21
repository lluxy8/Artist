using Core.Abstract;

namespace Core.Entities
{
    public class Page : BaseEntity
    {
        public required string DisplayName { get; set; }
        public required string UrlName { get; set; }
        public PageContent? PageContent { get; set; }
    }
}
