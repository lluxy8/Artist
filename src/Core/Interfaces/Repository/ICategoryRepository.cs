using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetHighlightedCategoriesAsync();
        Task<Category?> GetByUrlAsync(string url);
    }
}
