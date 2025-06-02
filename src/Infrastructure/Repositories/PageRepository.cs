using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class PageRepository : BaseRepository<Page>, IPageRepository
    {
        public PageRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<Page> IncludeRelatedEntities(IQueryable<Page> query)
        {
            return query
                .Include(p => p.PageContent)
                .ThenInclude(pc => pc.PageCarousels)
                .Include(p => p.PageContent)
                .ThenInclude(pc => pc.PageSections);
        }

        public async Task<Page?> GetByUrlAsync(string url)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await IncludeRelatedEntities(context.Pages)
                .Where(p => p.UrlName == url)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckUrl(string url)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();

            return await context.Categories.AnyAsync(x => x.UrlName == url);
        }
    }
}
