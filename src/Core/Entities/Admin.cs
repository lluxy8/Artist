using Core.Abstract;

namespace Core.Entities
{
    public class Admin : BaseEntity
    {
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }

        public List<LoginAttempt> LoginAttempts { get; set; } = [];
    }
}
