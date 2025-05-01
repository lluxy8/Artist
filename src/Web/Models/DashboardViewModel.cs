using Core.Entities;

namespace Web.Models
{
    public class DashboardViewModel
    {
        public int PageCount { get; set; }
        public int ProjectCount { get; set; }
        public int CategoryCount { get; set; }
        public int SocialCount { get; set; }
        public int LogCountToday { get; set; }
        public int LoginCountToday { get; set; }
        public List<Log> Logs { get; set; } = [];
        public List<LoginAttempt> LoginAttempts { get; set; } = [];
    }
}
