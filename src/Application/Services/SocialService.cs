using Application.Abstract;
using Core.Common.Helpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public sealed class SocialService : BaseService<Social>
    {
        public SocialService(IRepository<Social> repository) : base(repository)
        {
        }


        public async Task AddAsync(SocialCreateDto dto, HttpRequest request)
        {
            try
            {
                var file = dto.Image;

                string imgurl = await FileHelper.SaveImageAsync(file, "Social", request);


                var social = new Social
                {
                    IconUrl = imgurl,
                    Name = dto.Name,
                    Url = dto.Url
                };

                await _repository.AddAsync(social);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

        public async Task UpdateAsync(SocialUpdateDto dto, HttpRequest request)
        {
            try
            {
                var existingEntity = await _repository.GetByIdAsync(dto.Id)
                    ?? throw new Exception("Entity not found.");

                string imgurl = existingEntity.IconUrl;
                var file = dto.Image;
                var ChangeImg = file != null && file.Length > 0;

                if (ChangeImg)
                    imgurl = await FileHelper.SaveImageAsync(file, "Social", request);

                var social = new Social
                {
                    Id = existingEntity.Id,
                    IconUrl = imgurl,
                    Name = dto.Name,
                    Url = dto.Url,
                    CreateDate = existingEntity.CreateDate,
                    UpdateDate = DateTime.UtcNow
                };

                await _repository.UpdateAsync(social);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }
    }
}
