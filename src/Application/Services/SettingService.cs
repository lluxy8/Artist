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
    public class SettingService : BaseService<Setting>, ISettingService
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EventDispatcher _eventDispatcher;
        public SettingService(IRepository<Setting> repository,
            IAuthenticationManager authenticationManager,
            EventDispatcher eventDispatcher) : base(repository)
        {
            _authenticationManager = authenticationManager;
            _eventDispatcher = eventDispatcher;
        }

        public async Task UpdateAsync(SettingUpdateDto dto, HttpRequest request)
        {
            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Site ayarları bulunamadı.");

            string imgurl = existingEntity.LogoUrl;
            var file = dto.Image;
            var ChangeImg = file != null && file.Length > 0;

            if (ChangeImg)
                imgurl = await FileHelper.SaveImageAsync(file, "PageContent", request);

            var setting = new Setting
            {
                Id = dto.Id,
                LogoUrl = imgurl,
                CompanyName = dto.CompanyName,
                DoneCustomerCount = dto.DoneCustomerCount,
                DoneProjectsCount = dto.DoneProjectsCount,
                AddressGoogleMaps = dto.AddressGoogleMaps,
                ExperienceYear = dto.ExperienceYear,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Address = dto.Address,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(setting);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Site ayarları güncellendi.", LogType.Update));
        }
    }
}
