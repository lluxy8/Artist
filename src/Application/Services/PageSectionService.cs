using Application.Abstract;
using Application.Events;
using Core.Common.Dispatchers;
using Core.Common.Enums;
using Core.Common.Helpers;
using Core.Common.Models;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class PageSectionService : BaseService<PageSection>, IPageSectionService
    {
        private readonly IPageSectionRepository _psRepository;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EventDispatcher _eventDispatcher;
        public PageSectionService(IRepository<PageSection> repository, IPageSectionRepository psRepository, IAuthenticationManager authenticationManager, EventDispatcher eventDispatcher) : base(repository)
        {
            _psRepository = psRepository;
            _authenticationManager = authenticationManager;
            _eventDispatcher = eventDispatcher;
        }

        public async Task AddAsync(PageSectionCreateDto dto, HttpRequest request)
        {
            var hasImage = dto.Image is { Length: > 0 };
            var imgUrl = string.Empty;

            if (hasImage)
            {
                var file = dto.Image;

                imgUrl = await FileHelper.SaveImageAsync(file,
                    "ProjectImage", request);
            }

            var ps = new PageSection
            {
                ImageUrl = imgUrl,
                Text1 = dto.Text1 ?? string.Empty,
                Text2 = dto.Text2 ?? string.Empty,
                Text3 = dto.Text3 ?? string.Empty,
                ImagePosition = dto.ImagePosition,
                PageContentId = dto.PageContentId,
                DisplayOrder = 0
            };

            await _psRepository.AddAsync(ps);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Yeni bir sayfa bölümü olulşturuldu.", LogType.Create));
        }

        public async Task UpdateAsync(PageSectionUpdateDto dto, HttpRequest request)
        {
            var ex = await _psRepository.GetByIdAsync(dto.Id);

            if (ex is null)
                throw new Exception("Sayfa bölümü bulunamadı.");

            var hasImage = dto.Image is { Length: > 0 };
            var imgUrl = ex.ImageUrl;

            if (hasImage)
            {
                var file = dto.Image;

                imgUrl = await FileHelper.SaveImageAsync(file,
                    "ProjectImage", request);
            }

            var ps = new PageSection
            {
                Id = dto.Id,
                ImageUrl = imgUrl,
                Text1 = dto.Text1 ?? string.Empty,
                Text2 = dto.Text2 ?? string.Empty,
                Text3 = dto.Text3 ?? string.Empty,
                PageContentId = dto.PageContentId,
                PageContent = ex.PageContent,
                ImagePosition = dto.ImagePosition,
                DisplayOrder = dto.Order,
                CreateDate = ex.CreateDate,
                UpdateDate = DateTime.UtcNow
            };

            await _psRepository.UpdateAsync(ps);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Bir bölümü güncellendi.", LogType.Update));
        }

        public async Task<List<PageSection>> GetByPageContentAsync(Guid id)
        {
            return await _psRepository.GetByPageContentAsync(id);
        }

        public async Task UpdateOrders(List<DisplayOrderModel> items)
        {
            await _psRepository.UpdateOrders(items);
        }
    }
}
