using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface ISettingService : IService<Setting>
    {
        Task UpdateAsync(SettingUpdateDto dto, HttpRequest request);
    }
}
