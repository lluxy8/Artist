using Application.Abstract;
using Core.Entities;
using Core.Interfaces;
using Core.DTOs;
using Core.Common.Helpers;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public sealed class PageCarouselService : BaseService<PageCarousel>
    {
        public PageCarouselService(IRepository<PageCarousel> repository) : base(repository)
        {
        }

        public async Task AddAsync(PageCarouselCreateDto dto, HttpRequest request)
        {
            try
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

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

        public async Task UpdateAsync(PageCarouselUpdateDto dto, HttpRequest request)
        {
            try
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
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }

    }
}
