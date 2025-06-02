using Application.Abstract;
using Application.Events;
using Core.Common.Dispatchers;
using Core.Common.Enums;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Application.Services
{
    public sealed class ProjectService : BaseService<Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EventDispatcher _eventDispatcher;  
        public ProjectService(IRepository<Project> repository, IProjectRepository projectRepository,
            IAuthenticationManager authenticationManager,
            EventDispatcher eventDispatcher) 
            : base(repository)
        {
            _projectRepository = projectRepository;
            _authenticationManager = authenticationManager;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<List<Project>> GetHighlightedProjects()
        {
            return await _projectRepository.GetHighlightedProjectsAsync() ?? [];
        }

        public async Task<List<Project>> GetVisibleProjects()
        {
            return await _projectRepository.GetVisibleProjectsAsync() ?? [];
        }

        public async Task<List<Project>> GetBySubCategoryIdAsync(Guid id)
        {
            return await _projectRepository.GetBySubCategoryIdAsync(id) ?? [];
        }

        public async Task<List<Project>> GetByCategoryIdAsync(Guid id)
        {
            return await _projectRepository.GetByCategoryIdAsync(id);
        }

        public async Task AddAsync(ProjectCreateDto dto)
        {
            var project = new Project
            {
                DisplayName = dto.Name,
                Description = dto.Description,
                SubCategoryId = dto.CategoryId,
                IsHighlighted = dto.IsHighlighted,
                UrlName = dto.UrlName,
                Refference = dto.Reference,
                IsVisible = dto.IsVisible
            };

            await _repository.AddAsync(project);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Yeni bir proje oluşturuldu.", LogType.Create));
        }

        public async Task<Project?> GetByUrlAsync(string url)
        {
            return await _projectRepository.GetByUrl(url);
        }


        public async Task UpdateAsync(ProjectUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Proje bulunamadı.");

            var project = new Project
            {
                Id = dto.Id,
                DisplayName = dto.Name,
                UrlName = dto.UrlName,
                Description = dto.Description,
                IsVisible = dto.IsVisible,
                IsHighlighted = dto.IsHighlighted,
                Refference = dto.Reference,
                SubCategoryId = dto.CategoryId,
                SubCategory = existingEntity.SubCategory, 
                CreateDate = existingEntity.CreateDate,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(project);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                $"{existingEntity.DisplayName} Adlı proje güncellendi.", LogType.Update));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var existingEntity = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Proje bulunamadı.");

            await _repository.DeleteAsync(existingEntity);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                $"{existingEntity.DisplayName} Adlı proje silindi.", LogType.Delete));
        }
    }
}
