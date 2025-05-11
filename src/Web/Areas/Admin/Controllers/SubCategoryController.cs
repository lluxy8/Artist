using Core.DTOs;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        public SubCategoryController(ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(Guid scid)
        {
            var scs = await _subCategoryService.GetByCategoryIdAsync(scid);
            return View(scs);
        }

        public async Task<IActionResult> Create()
        {

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "DisplayName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _subCategoryService.AddAsync(dto, Request);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "DisplayName");
            return View(dto);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var sc = await _subCategoryService.GetByIdAsync(id);
            if (sc == null)
            {
                return NotFound();
            }

            var dto = new SubCategoryUpdateDto
            {
                Id = sc.Id,
                DisplayName = sc.DisplayName,
                UrlName = sc.UrlName,
                ImageUrl = sc.ImageUrl,
                CategoryId = sc.CategoryId
            };

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "DisplayName");
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, SubCategoryUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {

                await _subCategoryService.UpdateAsync(dto, Request);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "DisplayName");
            return View(dto);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var sc = await _subCategoryService.GetByIdAsync(id);
            if (sc == null)
            {
                return NotFound();
            }
            return View(sc);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var social = await _subCategoryService.GetByIdAsync(id);
            if (social != null)
            {
                await _subCategoryService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
