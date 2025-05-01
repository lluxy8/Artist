using Core.Common.Dispatchers;
using Core.Common.Authentication;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ISessionManager, SessionManager>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<EventDispatcher>();

            return services;
        }
    }
}
