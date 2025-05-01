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

            string imgurl = existingEntity.ImageUrl;
            var file = dto.Image;
            var ChangeImg = file != null && file.Length > 0;

            if (ChangeImg)
                imgurl = await FileHelper.SaveImageAsync(file, "PageContent", request);

            var pc = new PageContent
            {
                Id = dto.Id,
                ImageUrl = imgurl,
                Text1 = dto.Text1,
                Text2 = dto.Text2,
                Text3 = dto.Text3,
                PageCarousels = existingEntity.PageCarousels,
                PageId = dto.PageId,
                Page = existingEntity.Page,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(pc);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Bir sayfa içeriği güncellendi.", LogType.Update));
        }
    }
}
