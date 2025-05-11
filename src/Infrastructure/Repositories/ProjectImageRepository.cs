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
            using var context = _contextfactory.CreateDbContext();
            return (await context.Projects
                .AsNoTracking()
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == ProjectId))?.Images
                .Any(x => x.IsMainImage) 
                ?? false;
        }

    }
}
