using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Core.Common.Authentication
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetUserSession(Guid userId)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            session?.Set("UserId", Encoding.UTF8.GetBytes(userId.ToString()));
        }

        public void ClearSession()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }

        public Guid? GetUserId()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session?.TryGetValue("UserId", out var userIdBytes) == true)
            {
                var userIdStr = Encoding.UTF8.GetString(userIdBytes);
                return Guid.TryParse(userIdStr, out var userId) ? userId : null;
            }
            return null;
        }
    }

}
