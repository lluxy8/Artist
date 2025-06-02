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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SubCategoryService : BaseService<SubCategory>, ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EventDispatcher _eventDispatcher;
        public SubCategoryService(IRepository<SubCategory> repository,
            ISubCategoryRepository subCategoryRepository,
            IAuthenticationManager authenticationManager,
            EventDispatcher eventDispatcher) : base(repository)
        {
            _subCategoryRepository = subCategoryRepository;
            _eventDispatcher = eventDispatcher;
            _authenticationManager = authenticationManager;
        }


        public async Task<List<SubCategory>> GetByCategoryIdAsync(Guid id)
        {
            return await _subCategoryRepository.GetByCategoryIdAsync(id);
        }

        public async Task<SubCategory?> GetByUrlAsync(string url)
        {
            return await _subCategoryRepository.GetByUrlAsync(url);
        }

        public async Task AddAsync(SubCategoryCreateDto dto, HttpRequest request)
        {
            var entity = new SubCategory
            {
                DisplayName = dto.DisplayName,
                CategoryId = dto.CategoryId,
                UrlName = dto.UrlName,
                ImageUrl = await FileHelper.SaveImageAsync(dto.Image, "SubCategory", request)
            };

            await _subCategoryRepository.AddAsync(entity);

            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Yeni bir alt kategori oluşturuldu.", LogType.Create));
        }

        public async Task UpdateAsync(SubCategoryUpdateDto dto, HttpRequest request)
        {
            var existingEntity = await _subCategoryRepository.GetByIdAsync(dto.Id)
                                 ?? throw new Exception("Alt kategori bulunamadı.");

            string imgurl = existingEntity.ImageUrl;
            var file = dto.Image;
            var ChangeImg = file != null && file.Length > 0;

            if (ChangeImg)
                imgurl = await FileHelper.SaveImageAsync(file, "PageCarousel", request);

            var entity = new SubCategory
            {
                Id = dto.Id,
                DisplayName = dto.DisplayName,
                UrlName = dto.UrlName,
                CategoryId = dto.CategoryId,
                ImageUrl = imgurl,
                Category = existingEntity.Category,
                CreateDate = existingEntity.CreateDate,
                Projects = existingEntity.Projects,
                UpdateDate = DateTime.UtcNow
            };

            await _subCategoryRepository.UpdateAsync(entity);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Alt kategori güncellendi.", LogType.Update));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var entity = await _subCategoryRepository.GetByIdAsync(id)
                         ?? throw new Exception("Alt kategori bulunamadı.");

            await _subCategoryRepository.DeleteAsync(entity);
            await _eventDispatcher.DispatchAsync(new LogEvent(
                _authenticationManager.GetUser().Name,
                "Alt kategori silindi.", LogType.Delete));

        }
    }
}
