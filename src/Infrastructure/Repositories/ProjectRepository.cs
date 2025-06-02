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
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await IncludeRelatedEntities(context.Projects)
                .Where(p => p.IsHighlighted && p.IsVisible)
                .AsNoTrackingWithIdentityResolution()
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<List<Project>> GetVisibleProjectsAsync()
        {
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await IncludeRelatedEntities(context.Projects)
                .Where(p => p.IsVisible)
                .AsNoTrackingWithIdentityResolution()
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<List<Project>> GetBySubCategoryIdAsync(Guid id)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await IncludeRelatedEntities(context.Projects)
                .Where(p => p.SubCategoryId == id && p.IsVisible)
                .AsNoTrackingWithIdentityResolution()
                .AsSplitQuery()
                .ToListAsync();

        }

        public async Task<List<Project>> GetByCategoryIdAsync(Guid id)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await IncludeRelatedEntities(context.Projects)
                .Where(x => x.SubCategory.CategoryId == id)
                .AsNoTrackingWithIdentityResolution()
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Project?> GetByUrl(string url)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await IncludeRelatedEntities(context.Projects)
                .Where(p => p.UrlName == url && p.IsVisible)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }
    }
}
