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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public sealed class SocialService : BaseService<Social>, ISocialService
    {
        private readonly IAuthenticationManager _authenticationService;
        private readonly EventDispatcher _eventDispatcher;
        public SocialService(IRepository<Social> repository,
            IAuthenticationManager authenticationService,
            EventDispatcher eventDispatcher) : base(repository)
        {
            _authenticationService = authenticationService;
            _eventDispatcher = eventDispatcher;
        }


        public async Task AddAsync(SocialCreateDto dto, HttpRequest request)
        {


            var social = new Social
            {
                IconUrl = "",
                Name = dto.Name,
                Url = dto.Url
            };

            await _repository.AddAsync(social);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationService.GetUser().Name,
                "Yeni bir sosyal medya bağlantısı oluşturuldu.", LogType.Create));
        }

        public async Task UpdateAsync(SocialUpdateDto dto, HttpRequest request)
        {
            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Sosyal medya bağlantısı bulunamadı.");

            string imgurl = existingEntity.IconUrl;
            var file = dto.Image;
            var ChangeImg = file != null && file.Length > 0;

            if (ChangeImg)
                imgurl = await FileHelper.SaveImageAsync(file, "Social", request);

            var social = new Social
            {
                Id = existingEntity.Id,
                IconUrl = "",
                Name = dto.Name,
                Url = dto.Url,
                CreateDate = existingEntity.CreateDate,
                UpdateDate = DateTime.UtcNow
            };

            await _repository.UpdateAsync(social);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationService.GetUser().Name,
                $"{existingEntity.Name} Adlı sosyal medya bağlantısı güncellendi.", LogType.Update));   
        }

        public override async Task DeleteAsync(Guid id)
        {
            var existingEntity = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Sosyal medya bağlantısı bulunamadı.");

            await _repository.DeleteAsync(existingEntity);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationService.GetUser().Name,
                $"{existingEntity.Name} Adlı sosyal medya bağlantısı silindi.", LogType.Delete));
        }
    }
}
