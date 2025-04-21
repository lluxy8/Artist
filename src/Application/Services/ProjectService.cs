using Application.Abstract;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Application.Services
{
    public sealed class ProjectService : BaseService<Project>
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IRepository<Project> repository, IProjectRepository projectRepository) 
            : base(repository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<Project>> GetHighlightedProjects()
        {
            try
            {
                return await _projectRepository.GetHighlightedProjectsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving highlighted projects.", ex);
            }
        }

        public async Task<List<Project>> GetVisibleProjects()
        {
            try
            {
                return await _projectRepository.GetVisibleProjectsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving highlighted projects.", ex);
            }
        }

        public async Task<List<Project>> GetByCategoryIdAsync(Guid id)
        {
            try
            {
                return await _projectRepository.GetByCategoryIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the project by category ID.", ex);

            }
        }
    }
}
