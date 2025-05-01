using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface IProjectImageService : IService<ProjectImage>
    {
        Task AddAsync(ProjectImageCreateDto dto, HttpRequest request);
        Task UpdateAsync(ProjectImageUpdateDto dto, HttpRequest request);
    }
}
