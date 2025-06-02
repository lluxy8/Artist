using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectImageRepository : BaseRepository<ProjectImage>, IProjectImageRepository
    {
        public ProjectImageRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }
        protected override IQueryable<ProjectImage> IncludeRelatedEntities(IQueryable<ProjectImage> query)
        {
            return query
                .Include(p => p.Project);
        }
        public async Task<bool> CheckMainImage(Guid ProjectId)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await context.ProjectImages
                .Where(x => x.ProjectId == ProjectId)
                .AnyAsync(x => x.IsMainImage);
        }

    }
}
