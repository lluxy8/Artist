using Application.Abstract;
using Core.Common.Helpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public sealed class PageContentService : BaseService<PageContent>
    {
        public PageContentService(IRepository<PageContent> repository) : base(repository)
        {
        }

        public async Task UpdateAsync(PageContentUpdateDto dto, HttpRequest request)
        {
            try
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
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }
    }
}
