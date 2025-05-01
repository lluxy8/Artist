using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface IPageCarouselService : IService<PageCarousel>
    {
        Task AddAsync(PageCarouselCreateDto dto, HttpRequest request);
        Task UpdateAsync(PageCarouselUpdateDto dto, HttpRequest request);

    }
}
