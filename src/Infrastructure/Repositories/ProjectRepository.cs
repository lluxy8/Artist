using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<Project> IncludeRelatedEntities(IQueryable<Project> query)
        {
            return query
                .Include(p => p.SubCategory)
                .Include(p => p.Images);
        }

        public async Task<List<Project>> GetHighlightedProjectsAsync()
        {
            using var context = _contextfactory.CreateDbContext();
            return await IncludeRelatedEntities(context.Projects)
                .AsNoTracking()
                .Where(p => p.IsHighlighted && p.IsVisible)
                .ToListAsync();
        }

        public async Task<List<Project>> GetVisibleProjectsAsync()
        {
            using var context = _contextfactory.CreateDbContext();
            return await IncludeRelatedEntities(context.Projects)
                .AsNoTracking()
                .Where(p => p.IsVisible)
                .ToListAsync();
        }

        public async Task<List<Project>> GetByCategoryIdAsync(Guid id)
        {
            using var context = _contextfactory.CreateDbContext();
            return await IncludeRelatedEntities(context.Projects)
                .AsNoTracking()
                .Where(p => p.SubCategoryId == id && p.IsVisible)
                .ToListAsync();

        }

        public async Task<Project?> GetByUrl(string url)
        {
            using var context = _contextfactory.CreateDbContext();
            return await IncludeRelatedEntities(context.Projects)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UrlName == url && p.IsVisible);
        }
    }
}
