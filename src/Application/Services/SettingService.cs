using Application.Abstract;
using Core.Common.Helpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class SettingService : BaseService<Setting>
    {
        public SettingService(IRepository<Setting> repository) : base(repository)
        {
        }

        public async Task UpdateAsync(SettingUpdateDto dto, HttpRequest request)
        {
            try
            {
                var existingEntity = await _repository.GetByIdAsync(dto.Id)
                    ?? throw new Exception("Entity not found.");

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
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }
    }
}
