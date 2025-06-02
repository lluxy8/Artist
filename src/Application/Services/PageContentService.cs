using Application.Abstract;
using Application.Events;
using Core.Common.Dispatchers;
using Core.Common.Enums;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public sealed class PageContentService : BaseService<PageContent>, IPageContentService
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EventDispatcher _eventDispatcher;
        public PageContentService(IRepository<PageContent> repository,
            IAuthenticationManager authenticationManager,
            EventDispatcher eventDispatcher) : base(repository)
        {
            _eventDispatcher = eventDispatcher;
            _authenticationManager = authenticationManager;
        }

        public async Task UpdateAsync(PageContentUpdateDto dto, HttpRequest request)
        {
            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Entity not found.");

            var pc = new PageContent
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                PageCarousels = existingEntity.PageCarousels,
                PageId = dto.PageId,
                Page = existingEntity.Page,
                CreateDate = existingEntity.CreateDate,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(pc);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Bir sayfa içeriği güncellendi.", LogType.Update));
        }
    }
}
