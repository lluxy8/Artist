using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DevConnection")));

            services.Scan(scan => scan
                .FromAssemblyOf<AdminRepository>()
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime());


            /*
            services.AddScoped<IRepository<Admin>, AdminRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Log>, LogRepository>();
            services.AddScoped<IRepository<LoginAttempt>, LoginAttemptRepository>();
            services.AddScoped<IRepository<Page>, PageRepository>();
            services.AddScoped<IRepository<PageCarousel>, PageCarouselRepository>();
            services.AddScoped<IRepository<PageContent>, PageContentRepository>();
            services.AddScoped<IRepository<Project>, ProjectRepository>();
            services.AddScoped<IRepository<ProjectImage>, ProjectImageRepository>();
            services.AddScoped<IRepository<Setting>, SettingRepository>();
            services.AddScoped<IRepository<Social>, SocialRepository>();


            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            */
            return services;
        }
    }
}
