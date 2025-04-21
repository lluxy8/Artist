using Application.Abstract;
using Core.Common.Helpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public sealed class CategoryService : BaseService<Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IRepository<Category> repository, ICategoryRepository categoryRepository)
            : base(repository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetHighlightedCategoriesAsync()
        {
            try
            {
                return await _categoryRepository.GetHighlightedCategoriesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving highlighted categories.", ex);
            }
        }

        public async Task<Category?> GetByUrlAsync(string url)
        {
            try
            {
                return await _categoryRepository.GetByUrlAsync(url);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the category with URL", ex);
            }
        }

        public async Task AddAsync(CategoryCreateDto dto, HttpRequest request)
        {
            try
            {
                var file = dto.Image;

                string imgurl = await FileHelper.SaveImageAsync(file, "Category", request);

                var category = new Category
                {
                    DisplayName = dto.DisplayName,
                    UrlName = dto.UrlName,
                    ImageUrl = imgurl,
                    Description = dto.Description,
                    IsHighlighted = dto.IsHighlighted,
                    CreateDate = DateTime.UtcNow
                };

                await _repository.AddAsync(category);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

        public async Task UpdateAsync(CategoryUpdateDto dto, HttpRequest request)
        {
            try
            {
                var existingEntity = await _repository.GetByIdAsync(dto.Id)
                    ?? throw new Exception("Entity not found.");

                string imgurl = existingEntity.ImageUrl;
                var file = dto.Image;
                var ChangeImg = file != null && file.Length > 0;

                if (ChangeImg)
                    imgurl = await FileHelper.SaveImageAsync(file, "Category", request);

                var category = new Category
                {
                    Id = dto.Id,
                    CreateDate = existingEntity.CreateDate,
                    UrlName = dto.UrlName,
                    Description = dto.Description,
                    DisplayName = dto.DisplayName,
                    ImageUrl = imgurl,
                    IsHighlighted = dto.IsHighlighted,
                    Projects = existingEntity.Projects,
                    UpdateDate = DateTime.UtcNow
                };

                await _repository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }
    }
}
