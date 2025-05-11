using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubCategoryRespository : BaseRepository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRespository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<SubCategory> IncludeRelatedEntities(IQueryable<SubCategory> query)
        {
            return query
                .Include(p => p.Projects)
                .Include(p => p.Category);
        }


        public Task<List<SubCategory>> GetByCategoryIdAsync(Guid id)
        {
            var context = _contextfactory.CreateDbContext();

            return IncludeRelatedEntities(context.SubCategories)
                .AsNoTracking()
                .Where(p => p.CategoryId == id)
                .ToListAsync();
        }

        public Task<SubCategory?> GetByUrlAsync(string url)
        {
            var context = _contextfactory.CreateDbContext();

            return IncludeRelatedEntities(context.SubCategories)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UrlName == url);
        }
    }
}
