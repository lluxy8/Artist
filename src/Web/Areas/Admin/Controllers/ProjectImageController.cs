using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Core.DTOs;
using Core.Interfaces.Service;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProjectImageController : Controller
    {
        private readonly IProjectImageService _projectImageService;
        private readonly IProjectService _projectService;

        public ProjectImageController(IProjectImageService projectImageService, IProjectService projectService)
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
            if(ModelState.IsValid)
            {
                await _projectImageService.AddAsync(projectImage, Request);
                return RedirectToAction("Images", "Project", new { id = projectImage.ProjectId });
            }

            return View(projectImage);
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
                return NotFound();

            if (ModelState.IsValid)
            {
                await _projectImageService.UpdateAsync(dto, Request);
                return RedirectToAction("Images", "Project", new { id = dto.ProjectId });
            }

            return View(dto);

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
                await _projectImageService.DeleteAsync(id);
                return RedirectToAction("Images", "Project", new { id = projectId });
            }
            return RedirectToAction("Index", "Project");
        }
    }
} 