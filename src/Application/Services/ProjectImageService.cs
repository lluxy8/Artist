using Application.Abstract;
using Core.Common.Helpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public sealed class ProjectImageService : BaseService<ProjectImage>
    {
        public ProjectImageService(IRepository<ProjectImage> repository) : base(repository)
        {
        }

        public async Task AddAsync(ProjectImageCreateDto dto, HttpRequest request)
        {
            try
            {
                var file = dto.Image;

                string imgurl = await FileHelper.SaveImageAsync(file, "PageCarousel", request);

                var pi = new ProjectImage
                {
                    IsMainImage = dto.IsMainImage,
                    ProjectId = dto.ProjectId,
                    Url = imgurl
                };

                await _repository.AddAsync(pi);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

        public async Task UpdateAsync(ProjectImageUpdateDto dto, HttpRequest request)
        {
            try
            {
                var existingEntity = await _repository.GetByIdAsync(dto.Id)
                    ?? throw new Exception("Entity not found.");

                string imgurl = existingEntity.Url;
                var file = dto.Image;
                var ChangeImg = file != null && file.Length > 0;

                if (ChangeImg)
                    imgurl = await FileHelper.SaveImageAsync(file, "PageCarousel", request);

                var pi = new ProjectImage
                {
                    Id = dto.Id,
                    IsMainImage = dto.IsMainImage,
                    ProjectId = dto.ProjectId,
                    Url = imgurl,
                    CreateDate = dto.CreateDate,
                    Project = dto.Project,
                    UpdateDate = DateTime.UtcNow
                };

                await _repository.UpdateAsync(pi);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }
    }
}
