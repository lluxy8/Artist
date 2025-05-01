using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface ISocialService : IService<Social>
    {
        Task AddAsync(SocialCreateDto dto, HttpRequest request);
        Task UpdateAsync(SocialUpdateDto dto, HttpRequest request);

    }
}
