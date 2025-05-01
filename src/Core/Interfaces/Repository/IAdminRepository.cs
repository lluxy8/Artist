using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin?> FindAdminAsync(string username, string password);
        Task<Admin?> GetByUserNameAsync(string username);
    }
}
