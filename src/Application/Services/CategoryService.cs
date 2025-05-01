using Application.Abstract;
using Application.Events;
using Core.Common.Dispatchers;
using Core.Common.Helpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Core.Common.Enums;

namespace Application.Services
{
    public sealed class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly EventDispatcher _eventDispatcher;
        private readonly IAuthenticationManager _authenticationManager;

        public CategoryService(IRepository<Category> repository, 
            ICategoryRepository categoryRepository,
            EventDispatcher eventDispatcher,
            IAuthenticationManager authenticationManager)
            : base(repository)
        {
            _categoryRepository = categoryRepository;
            _eventDispatcher = eventDispatcher;
            _authenticationManager = authenticationManager;
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
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Yeni bir kategori oluşturuldu.", LogType.Create));

        }

        public async Task UpdateAsync(CategoryUpdateDto dto, HttpRequest request)
        {
            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Kategori bulunamadı.");

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
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                $"{category.DisplayName} Adlı kategori güncellendi.", LogType.Update));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var existingEntity = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Kategori bulunamadı.");

            await _repository.DeleteAsync(existingEntity);

            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                $"{existingEntity.DisplayName} Adlı kategori silindi.", LogType.Delete));
        }
    }
}
