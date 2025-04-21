using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetHighlightedCategoriesAsync();
        Task<Category?> GetByUrlAsync(string url);
    }
}
