using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Core.DTOs;
using Core.Interfaces.Service;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public CategoryController(ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto category)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.AddAsync(category, Request);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var dto = new CategoryUpdateDto
            {
                Id = category.Id,
                ImageUrl = category.ImageUrl,
                CreateDate = category.CreateDate,
                Description = category.Description,
                UpdateDate = category.UpdateDate,
                DisplayName = category.DisplayName,
                UrlName = category.UrlName,
                IsHighlighted = category.IsHighlighted
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CategoryUpdateDto category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(category, Request);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null)
            {
                await _categoryService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Highlighted()
        {
            var highlightedCategories = await _categoryService.GetHighlightedCategoriesAsync();
            return View(highlightedCategories);
        }
    }
} 