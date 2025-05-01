using Application.Abstract;
using Core.Entities;
using Core.DTOs;
using Core.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Interfaces;
using Core.Common.Dispatchers;
using Application.Events;
using Core.Common.Enums;

namespace Application.Services
{
    public sealed class PageCarouselService : BaseService<PageCarousel>, IPageCarouselService
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EventDispatcher _eventDispatcher;
        public PageCarouselService(IRepository<PageCarousel> repository,
            IAuthenticationManager authenticationManager,
            EventDispatcher eventDispatcher) : base(repository)
        {
            _authenticationManager = authenticationManager;
            _eventDispatcher = eventDispatcher;
        }

        public async Task AddAsync(PageCarouselCreateDto dto, HttpRequest request)
        {
            var file = dto.Image;

            string imgurl = await FileHelper.SaveImageAsync(file, "PageCarousel", request);

            var pageCarousel = new PageCarousel
            {
                Id = Guid.NewGuid(),
                PageContentId = dto.PageContentId,
                ImageUrl = imgurl,
                Text1 = dto.Text1,
                Text2 = dto.Text2,
                Text3 = dto.Text3,
                CreateDate = DateTime.UtcNow
            };

            await _repository.AddAsync(pageCarousel);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Yeni bir sayfa carouseli oluşturuldu.", LogType.Create));
        }

        public async Task UpdateAsync(PageCarouselUpdateDto dto, HttpRequest request)
        {
            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Entity not found.");

            string imgurl = existingEntity.ImageUrl;
            var file = dto.Image;
            var ChangeImg = file != null && file.Length > 0;

            if (ChangeImg)
                imgurl = await FileHelper.SaveImageAsync(file, "PageCarousel", request);

            var pageCarousel = new PageCarousel
            {
                Id = existingEntity.Id,
                Text1 = dto.Text1,
                Text2 = dto.Text2,
                Text3 = dto.Text3,
                ImageUrl = imgurl,
                PageContentId = dto.PageContentId,
                PageContent = existingEntity.PageContent,
                CreateDate = existingEntity.CreateDate,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(pageCarousel);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Bir sayfa carouseli güncellendi.", LogType.Update));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var existingEntity = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Öğe bulunamadı.");

            await _repository.DeleteAsync(existingEntity);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Bir sayfa carouseli silindi.", LogType.Delete));
        }

    }
}
