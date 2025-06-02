using Core.Common.Models;
using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IPageSectionRepository : IRepository<PageSection>
    {
        Task<List<PageSection>> GetByPageContentAsync(Guid id);
        Task UpdateOrders(List<DisplayOrderModel> items);
    }
}
