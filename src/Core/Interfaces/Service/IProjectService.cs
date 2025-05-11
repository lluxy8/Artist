using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Service;
using System.Threading.Tasks;

namespace Core.Interfaces.Service
{
    public interface IProjectService : IService<Project>
    {
        Task UpdateAsync(ProjectUpdateDto dto);
        Task<Project?> GetByUrlAsync(string url);
        Task AddAsync(ProjectCreateDto dto);
        Task<List<Project>> GetHighlightedProjects();
        Task<List<Project>> GetVisibleProjects();
        Task<List<Project>> GetByCategoryIdAsync(Guid id);
    }
}
