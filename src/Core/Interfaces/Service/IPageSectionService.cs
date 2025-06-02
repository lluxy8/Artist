using Core.Common.Models;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface IPageSectionService : IService<PageSection>
    {
        Task AddAsync(PageSectionCreateDto dto, HttpRequest request);
        Task UpdateAsync(PageSectionUpdateDto dto, HttpRequest request);
        Task<List<PageSection>> GetByPageContentAsync(Guid id);
        Task UpdateOrders(List<DisplayOrderModel> items);
    }
}
