using Core.Common.Models;

namespace Core.Interfaces.Repository
{
    public interface IPageCarouselRepository
    {
        Task UpdateOrders(List<DisplayOrderModel> items);
    }
}
