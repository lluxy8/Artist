using Core.Entities;
using Core.Interfaces.Service;
using Core.DTOs;

namespace Core.Interfaces.Service
{
    public interface IAdminService : IService<Admin>
    {
        Task AddAsync(AdminCreateDto dto);
        Task UpdateAsync(AdminUpdateDto dto);
        Task<bool> LoginAsync(LoginDto dto);
    }
}
