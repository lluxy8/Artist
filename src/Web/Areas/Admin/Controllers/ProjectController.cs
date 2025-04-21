using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly CategoryService _categoryService;

        public ProjectController(ProjectService projectService, CategoryService categoryService)
        {
            _projectService = projectService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllAsync(true);
            return View(projects);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "DisplayName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            await _projectService.AddAsync(project);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "DisplayName");
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            await _projectService.UpdateAsync(project);
            return RedirectToAction(nameof(Index));
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
                await _projectService.DeleteAsync(project);
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