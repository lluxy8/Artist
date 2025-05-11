using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<List<SubCategory>> GetByCategoryIdAsync(Guid id);
        Task<SubCategory?> GetByUrlAsync(string url);
    }
}
