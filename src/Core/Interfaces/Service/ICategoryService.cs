using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface ICategoryService : IService<Category>
    {
        Task<List<Category>> GetHighlightedCategoriesAsync();
        Task<Category?> GetByUrlAsync(string url);
        Task AddAsync(CategoryCreateDto dto, HttpRequest request);
        Task UpdateAsync(CategoryUpdateDto dto, HttpRequest request);
    }
}
