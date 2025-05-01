using Core.Abstract;

namespace Core.Entities
{
    public class Log : BaseEntity
    {
        public int Type { get; set; }
        public required string Message { get; set; }
    }
}
