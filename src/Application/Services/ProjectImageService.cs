using Application.Abstract;
using Application.Events;
using Core.Common.Dispatchers;
using Core.Common.Enums;
using Core.Common.Helpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public sealed class ProjectImageService : BaseService<ProjectImage>, IProjectImageService
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EventDispatcher _eventDispatcher;
        public ProjectImageService(IRepository<ProjectImage> repository,
            IAuthenticationManager authenticationManager,
            EventDispatcher eventDispatcher) : base(repository)
        {
            _authenticationManager = authenticationManager;
            _eventDispatcher = eventDispatcher;
        }

        public async Task AddAsync(ProjectImageCreateDto dto, HttpRequest request)
        {
            var file = dto.Image;

            string imgurl = await FileHelper.SaveImageAsync(file, "PageCarousel", request);

            var pi = new ProjectImage
            {
                IsMainImage = dto.IsMainImage,
                ProjectId = dto.ProjectId,
                Url = imgurl
            };

            await _repository.AddAsync(pi);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Yeni bir proje resmi oluşturuldu.", LogType.Create));
        }

        public async Task UpdateAsync(ProjectImageUpdateDto dto, HttpRequest request)
        {
            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Entity not found.");

            string imgurl = existingEntity.Url;
            var file = dto.Image;
            var ChangeImg = file != null && file.Length > 0;

            if (ChangeImg)
                imgurl = await FileHelper.SaveImageAsync(file, "PageCarousel", request);

            var pi = new ProjectImage
            {
                Id = dto.Id,
                IsMainImage = dto.IsMainImage,
                ProjectId = dto.ProjectId,
                Url = imgurl,
                CreateDate = dto.CreateDate,
                Project = dto.Project,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(pi);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Bir proje resmi güncellendi.", LogType.Update));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var existingEntity = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Öğe bulunamadı.");

            await _repository.DeleteAsync(existingEntity);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Bir proje resmi silindi.", LogType.Delete));

        }
    }
}
