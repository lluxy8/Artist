using Core.Common.Models;
using Core.Entities;
using Core.Interfaces.Repository;
using EFCore.BulkExtensions;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PageSectionRepository : BaseRepository<PageSection>, IPageSectionRepository
    {
        public PageSectionRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<List<PageSection>> GetByPageContentAsync(Guid id)
        { 
            await using var context = await _contextfactory.CreateDbContextAsync();

            return await context.PageSections
                .Where(x => x.PageContentId == id)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }


        public async Task UpdateOrders(List<DisplayOrderModel> items)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();

            var ids = items.Select(x => x.Id).ToList();

            var sections = await context.PageSections
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();

            var sectionList = new List<PageSection>();

            var itemDict = items
                .ToDictionary(x => x.Id, x => x.Order);

            foreach (var section in sections)
            {
                if (!itemDict.TryGetValue(section.Id, out var newOrder)
                    || section.DisplayOrder == newOrder) continue;

                section.DisplayOrder = newOrder;
                sectionList.Add(section);
            }

            await context.SaveChangesAsync();
        }
    }
}
