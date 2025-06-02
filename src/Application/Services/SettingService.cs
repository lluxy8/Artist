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

            var logoUrl = existingEntity.LogoUrl;
            var logoFile = dto.Image;
            var changeImg = logoFile is { Length: > 0 };


            if (changeImg)
                logoUrl = await FileHelper.SaveImageAsync(logoFile, "PageContent", request);

            var setting = new Setting
            {
                Id = dto.Id,
                LogoUrl = logoUrl,
                CompanyName = dto.CompanyName,
                DoneCustomerCount = dto.DoneCustomerCount,
                DoneProjectsCount = dto.DoneProjectsCount,
                AddressGoogleMaps = dto.AddressGoogleMaps,
                ExperienceYear = dto.ExperienceYear,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Address = dto.Address,
                PrimaryColor = dto.PrimaryColor,
                SecondaryColor = dto.SecondaryColor,
                ThirdColor = dto.ThirdColor,
                CreateDate = existingEntity.CreateDate,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(setting);

            if (dto.Icon != null)
                await FileHelper.SaveIconAsync(dto.Icon);

            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Site ayarları güncellendi.", LogType.Update));
        }
    }
}
