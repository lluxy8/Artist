using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<AdminService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<PageService>();
            services.AddScoped<PageCarouselService>();
            services.AddScoped<PageContentService>();
            services.AddScoped<SettingService>();
            services.AddScoped<SocialService>();
            services.AddScoped<LogService>();
            services.AddScoped<LoginAttemptService>();
            services.AddScoped<ProjectImageService>();
            services.AddScoped<ProjectService>();

            services.AddScoped<IService<Admin>, AdminService>();
            services.AddScoped<IService<Category>, CategoryService>();
            services.AddScoped<IService<Log>, LogService>();
            services.AddScoped<IService<LoginAttempt>, LoginAttemptService>();
            services.AddScoped<IService<Page>, PageService>();
            services.AddScoped<IService<PageCarousel>, PageCarouselService>();
            services.AddScoped<IService<PageContent>, PageContentService>();
            services.AddScoped<IService<Project>, ProjectService>();
            services.AddScoped<IService<ProjectImage>, ProjectImageService>();
            services.AddScoped<IService<Setting>, SettingService>();
            services.AddScoped<IService<Social>, SocialService>();


            return services;
        }
    }
}
