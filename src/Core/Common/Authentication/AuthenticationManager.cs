using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Core.Interfaces;
using Core.Common.Models;

namespace Core.Common.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(UserAuthModel model)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new(ClaimTypes.Name, model.Name),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public UserAuthModel GetUser()
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usernameStr = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            return new UserAuthModel
            {
                Id = Guid.TryParse(userIdStr, out var userId) ? userId : Guid.Empty,
                Name = usernameStr ?? string.Empty
            };
        }
    }
}
