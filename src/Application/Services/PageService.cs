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
    public sealed class PageService : BaseService<Page>, IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EventDispatcher _eventDispatcher;
        public PageService(IRepository<Page> repository, IPageRepository pageRepository,
            IAuthenticationManager authenticationManager,
            EventDispatcher eventDispatcher) : base(repository)
        {
            _pageRepository = pageRepository;
            _authenticationManager = authenticationManager;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Page?> GetByUrlAsync(string url)
        {
            return await _pageRepository.GetByUrlAsync(url);
        }

        public async Task AddAsync(PageCreateDto dto)
        {
            if (await _pageRepository.CheckUrl(dto.UrlName))
                throw new Exception("Bu url'ye sahip bir sayfa zaten var!");

            var page = new Page
            {
                DisplayName = dto.DisplayName,
                UrlName = dto.UrlName,
                ListCategories = dto.ListCategories,
                ListProjects = dto.ListProjects
            };

            await _repository.AddAsync(page);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Yeni bir sayfa oluşturuldu.", LogType.Create));
        }

        public async Task UpdateAsync(PageUpdateDto dto)
        {
            if (await _pageRepository.CheckUrl(dto.UrlName))
                throw new Exception("Bu url'ye sahip bir sayfa zaten var!");

            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Sayfa bulunamadı."); ;

            var page = new Page
            {
                Id = dto.Id,
                DisplayName = dto.DisplayName,
                UrlName = dto.UrlName,
                ListCategories = dto.ListCategories,
                ListProjects = dto.ListProjects,
                CreateDate = existingEntity.CreateDate,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(page);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                $"{existingEntity.DisplayName} Adlı sayfa güncellendi.", LogType.Update));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var existingEntity = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Sayfa bulunamadı.");

            await _repository.DeleteAsync(existingEntity);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                $"{existingEntity.DisplayName} Adlı sayfa silindi.", LogType.Delete));

            
        }

    }
}
