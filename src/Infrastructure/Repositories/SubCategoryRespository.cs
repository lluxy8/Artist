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


        public async Task<List<SubCategory>> GetByCategoryIdAsync(Guid id)
        {
            var context = await _contextfactory.CreateDbContextAsync();

            return await IncludeRelatedEntities(context.SubCategories)
                .Where(p => p.CategoryId == id)
                .AsNoTrackingWithIdentityResolution()
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<SubCategory?> GetByUrlAsync(string url)
        {
            var context = await _contextfactory.CreateDbContextAsync();

            return await IncludeRelatedEntities(context.SubCategories)
                .Where(p => p.UrlName == url)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }


    }
}
