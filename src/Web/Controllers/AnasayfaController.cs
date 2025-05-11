using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AnasayfaController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IProjectService _projectService;
        private readonly ICategoryService _categoryService;

        public AnasayfaController(
            IPageService pageService,
            IProjectService projectService,
            ICategoryService categoryService)
        {
            _pageService = pageService;
            _projectService = projectService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetHighlightedProjects();
            var categories = await _categoryService.GetHighlightedCategoriesAsync();
            var page = await _pageService.GetByUrlAsync("anasayfa");

            if (page is null)
                return NotFound();

            var vm = new PageViewModel
            {
                Page = page,
                Projects = projects,
                Categories = categories
            };

            return View(vm);
        }
    }
}
