using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public sealed class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<Category> IncludeRelatedEntities(IQueryable<Category> query)
        {
            return query
                .Include(p => p.SubCategories);
        }

        public async Task<List<Category>> GetHighlightedCategoriesAsync()
        {
            await using var context = await _contextfactory.CreateDbContextAsync();

            return await IncludeRelatedEntities(context.Categories)
                .Where(c => c.IsHighlighted)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<Category?> GetByUrlAsync(string url)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();

            return await IncludeRelatedEntities(context.Categories)
                .Where(c => c.UrlName == url)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckUrl(string url)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();

            return await context.Categories
                .AnyAsync(x => x.UrlName == url);
        }
    }
}
