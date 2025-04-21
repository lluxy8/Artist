using Core.Entities;

namespace Core.Interfaces
{
    public interface IAdminRepository
    {
        Task<bool> FindAdminAsync(string username, string password);
        Task<Admin?> GetByUserNameAsync(string username);
    }
}
