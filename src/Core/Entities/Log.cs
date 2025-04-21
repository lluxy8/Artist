using Core.Abstract;

namespace Core.Entities
{
    public class Log : BaseEntity
    {
        public required string Message { get; set; }
    }
}
