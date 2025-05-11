using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface ISubCategoryService : IService<SubCategory>
    {
        Task<List<SubCategory>> GetByCategoryIdAsync(Guid id);
        Task<SubCategory?> GetByUrlAsync(string url);
        Task AddAsync(SubCategoryCreateDto dto, HttpRequest request);
        Task UpdateAsync(SubCategoryUpdateDto dto, HttpRequest request);
    }
}
