using Core.Common.Models;
using Core.Entities;
using Core.Interfaces.Repository;
using EFCore.BulkExtensions;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PageCarouselRepository : BaseRepository<PageCarousel>, IPageCarouselRepository
    {
        public PageCarouselRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<PageCarousel> IncludeRelatedEntities(IQueryable<PageCarousel> query)
        {
            return query
                .Include(p => p.PageContent);
        }

        public async Task UpdateOrders(List<DisplayOrderModel> items)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();

            var ids = items.Select(x => x.Id).ToList();

            var carousels = await context.PageCarousels
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();

            var carouselList = new List<PageCarousel>();

            var itemDict = items
                .ToDictionary(x => x.Id, x => x.Order);

            foreach (var carousel in carousels)
            {
                if (!itemDict.TryGetValue(carousel.Id, out var newOrder) 
                    || carousel.DisplayOrder == newOrder) continue;

                carousel.DisplayOrder = newOrder;
                carouselList.Add(carousel);
            }

            await context.SaveChangesAsync();
        }

    }
}
