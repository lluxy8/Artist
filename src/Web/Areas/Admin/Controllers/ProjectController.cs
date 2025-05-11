using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Interfaces.Service;
using Core.DTOs;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        public ProjectController(IProjectService projectService, ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            _projectService = projectService;
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()    
        {
            var projects = await _projectService.GetAllAsync(true);
            return View(projects);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.SubCategories = await _subCategoryService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDto project)
        {
            if(ModelState.IsValid)
            {
                await _projectService.AddAsync(project);
                return RedirectToAction(nameof(Index));
            }


            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.SubCategories = await _subCategoryService.GetAllAsync();
            return View(project);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var subCategory = await _subCategoryService.GetByIdAsync(project.SubCategoryId, true);

            var dto = new ProjectUpdateDto
            {
                Id = project.Id,
                Name = project.DisplayName,
                UrlName = project.UrlName,
                Description = project.Description,
                CategoryId = project.SubCategoryId,
                IsHighlighted = project.IsHighlighted,
                SelectedCategoryId = subCategory is null ? Guid.Empty : subCategory.Category.Id,
                SelectedSubCategoryId = project.SubCategoryId,
                IsVisible = project.IsVisible
            };

            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.SubCategories = await _subCategoryService.GetAllAsync();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProjectUpdateDto project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await _projectService.UpdateAsync(project);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.SubCategories = await _subCategoryService.GetAllAsync();
            return View(project);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id, true);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project != null)
            {
                await _projectService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Images(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id, true);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
    }
} 