using Core.Entities;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PageCarouselRepository : BaseRepository<PageCarousel>
    {
        public PageCarouselRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<PageCarousel> IncludeRelatedEntities(IQueryable<PageCarousel> query)
        {
            return query
                .Include(p => p.PageContent);
        }
    }
}
