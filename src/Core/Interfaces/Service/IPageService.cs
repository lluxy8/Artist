using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Service;

namespace Core.Interfaces.Service
{
    public interface IPageService : IService<Page>
    {
        Task AddAsync(PageCreateDto dto);
        Task UpdateAsync(PageUpdateDto dto);
        Task<Page?> GetByUrlAsync(string url);
    }
}
