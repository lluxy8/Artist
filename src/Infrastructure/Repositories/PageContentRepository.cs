using Core.Entities;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PageContentRepository : BaseRepository<PageContent>
    {
        public PageContentRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<PageContent> IncludeRelatedEntities(IQueryable<PageContent> query)
        {
            return query
                .Include(p => p.PageCarousels)
                .Include(p => p.Page);
        }
    }
}
