using Core.Entities;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectImageRepository : BaseRepository<ProjectImage>
    {
        public ProjectImageRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<ProjectImage> IncludeRelatedEntities(IQueryable<ProjectImage> query)
        {
            return query
                .Include(p => p.Project);
        }
    }
}
