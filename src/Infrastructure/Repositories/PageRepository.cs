using Core.Entities;
using Core.Interfaces;
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
                .Include(p => p.PageContent.PageCarousels);
        }

        public async Task<Page?> GetByUrlAsync(string url)
        {
            using var context = _contextfactory.CreateDbContext();
            return await IncludeRelatedEntities(context.Pages)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UrlName == url);
        }
    }
}
