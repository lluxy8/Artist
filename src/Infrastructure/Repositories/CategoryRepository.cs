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
            using var context = _contextfactory.CreateDbContext();

            return await IncludeRelatedEntities(context.Categories)
                .AsNoTracking()
                .Where(c => c.IsHighlighted)          
                .ToListAsync();
        }

        public async Task<Category?> GetByUrlAsync(string url)
        {
            using var context = _contextfactory.CreateDbContext();

            return await IncludeRelatedEntities(context.Categories)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UrlName == url);
        }

        public async Task<bool> CheckUrl(string url)
        {
            using var context = _contextfactory.CreateDbContext();

            return await context.Categories.AnyAsync(x => x.UrlName == url);
        }
    }
}
