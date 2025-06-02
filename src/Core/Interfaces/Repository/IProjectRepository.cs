using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<Project>> GetHighlightedProjectsAsync();
        Task<Project?> GetByUrl(string url);
        Task<List<Project>> GetVisibleProjectsAsync();
        Task<List<Project>> GetBySubCategoryIdAsync(Guid id);
        Task<List<Project>> GetByCategoryIdAsync(Guid id);

    }
}
