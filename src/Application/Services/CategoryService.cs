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
        private readonly ISubCategoryRepository _subCategoryRepository;

        public CategoryService(IRepository<Category> repository, 
            ICategoryRepository categoryRepository,
            EventDispatcher eventDispatcher,
            IAuthenticationManager authenticationManager,
            ISubCategoryRepository subCategoryRepository)
            : base(repository)
        {
            _categoryRepository = categoryRepository;
            _eventDispatcher = eventDispatcher;
            _authenticationManager = authenticationManager;
            _subCategoryRepository = subCategoryRepository;
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
            if (await _categoryRepository.CheckUrl(dto.UrlName))
                throw new Exception("Bu url'ye sahip bir kategori zaten var!");

            var file = dto.Image;

            string imgurl = await FileHelper.SaveImageAsync(file, "Category", request);

            var category = new Category
            {
                Id = Guid.NewGuid(),
                DisplayName = dto.DisplayName,
                UrlName = dto.UrlName,
                ImageUrl = imgurl,
                Description = dto.Description,
                IsHighlighted = dto.IsHighlighted,
                CreateDate = DateTime.UtcNow
            };

            await _repository.AddAsync(category);
            await _subCategoryRepository.AddAsync(new SubCategory
            {
                DisplayName = "Tümü",
                UrlName = $"{category.UrlName}-kategorisiz",
                CategoryId = category.Id,
                ImageUrl = "https://community.softr.io/uploads/db9110/original/2X/7/74e6e7e382d0ff5d7773ca9a87e6f6f8817a68a6.jpeg",
            });

            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Yeni bir kategori oluşturuldu.", LogType.Create));

        }

        public async Task UpdateAsync(CategoryUpdateDto dto, HttpRequest request)
        {
            if (await _categoryRepository.CheckUrl(dto.UrlName))
                throw new Exception("Bu url'ye sahip bir kategori zaten var!");

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
                SubCategories = existingEntity.SubCategories,
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
