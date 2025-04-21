using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Core.DTOs;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
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
            await _categoryService.AddAsync(category, Request);
            return RedirectToAction(nameof(Index));
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

            await _categoryService.UpdateAsync(category, Request);
            return RedirectToAction(nameof(Index));
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
                await _categoryService.DeleteAsync(category);
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