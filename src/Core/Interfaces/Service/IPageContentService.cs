using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface IPageContentService : IService<PageContent>
    {
        Task UpdateAsync(PageContentUpdateDto dto, HttpRequest request);
    }
}
