using Application.Abstract;
using Application.Events;
using Application.Events.Handlers;
using Application.Interceptors;
using Application.Services;
using Castle.DynamicProxy;
using Core.Interfaces;
using Core.Interfaces.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler<LogEvent>, LogEventHandler>();

            services.AddSingleton<ProxyGenerator>();
            services.AddScoped<IInterceptor, ErrorhandlingInterceptor>();


            services.Scan(scan => scan
            .FromAssemblyOf<AdminService>()
            .AddClasses(classes => classes.AssignableTo(typeof(IService<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

            
            Assembly assembly = typeof(AdminService).Assembly;
            var baseGenericType = typeof(BaseService<>);

            var serviceTypes = assembly.GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.BaseType != null &&
                    t.BaseType.IsGenericType &&
                    t.BaseType.GetGenericTypeDefinition() == baseGenericType
                )
                .ToList();


            foreach (var serviceType in serviceTypes)
            {
                var interfaceTypes = serviceType.GetInterfaces();

                foreach (var interfaceType in interfaceTypes)
                {
                    services.AddScoped(interfaceType, provider =>
                    {
                        var proxyGenerator = provider.GetRequiredService<ProxyGenerator>();
                        var interceptor = provider.GetRequiredService<IInterceptor>();
                        var service = provider.GetRequiredService(serviceType);

                        return proxyGenerator.CreateInterfaceProxyWithTarget(interfaceType, service, interceptor);
                    });
                }
            }

            return services;
        }
    }
}
