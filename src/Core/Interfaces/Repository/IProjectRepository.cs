using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<Project>> GetHighlightedProjectsAsync();
        Task<List<Project>> GetVisibleProjectsAsync();
        Task<List<Project>> GetByCategoryIdAsync(Guid id);
    }
}
