using Core.Abstract;

namespace Core.Entities
{
    public class Social : BaseEntity
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string IconUrl { get; set; }
    }
}
