using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Core.DTOs;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProjectImageController : Controller
    {
        private readonly ProjectImageService _projectImageService;
        private readonly ProjectService _projectService;

        public ProjectImageController(ProjectImageService projectImageService, ProjectService projectService)
        {
            _projectImageService = projectImageService;
            _projectService = projectService;
        }

        public async Task<IActionResult> Create(Guid projectId)
        {
            var project = await _projectService.GetByIdAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;

            return View(new ProjectImageCreateDto { ProjectId = projectId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectImageCreateDto projectImage)
        {
            await _projectImageService.AddAsync(projectImage, Request);
            return RedirectToAction("Images", "Project", new { id = projectImage.ProjectId });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var projectImage = await _projectImageService.GetByIdAsync(id, true);
            if (projectImage == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetByIdAsync(projectImage.ProjectId);
            ViewBag.Project = project;

            var dto = new ProjectImageUpdateDto
            {
                Id = projectImage.Id,
                ProjectId = projectImage.ProjectId,
                Url = projectImage.Url,
                IsMainImage = projectImage.IsMainImage
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProjectImageUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            await _projectImageService.UpdateAsync(dto, Request);
            return RedirectToAction("Images", "Project", new { id = dto.ProjectId });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var projectImage = await _projectImageService.GetByIdAsync(id);
            if (projectImage == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetByIdAsync(projectImage.ProjectId);
            ViewBag.Project = project;
            return View(projectImage);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var projectImage = await _projectImageService.GetByIdAsync(id);
            if (projectImage != null)
            {
                var projectId = projectImage.ProjectId;
                await _projectImageService.DeleteAsync(projectImage);
                return RedirectToAction("Images", "Project", new { id = projectId });
            }
            return RedirectToAction("Index", "Project");
        }
    }
} 