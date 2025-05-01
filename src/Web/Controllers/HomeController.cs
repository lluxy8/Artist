using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Application.Services;
using Core.Entities;
using Core.Interfaces.Service;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPageService _pageService;
        private readonly IProjectService _projectService;
        private readonly ICategoryService _categoryService;
        private readonly ISettingService _settingService;
        private readonly ISocialService _socialService;

        public HomeController(
            ILogger<HomeController> logger,
            IPageService pageService,
            IProjectService projectService,
            ICategoryService categoryService,
            ISettingService settingService,
            ISocialService socialService)
        {
            _logger = logger;
            _pageService = pageService;
            _projectService = projectService;
            _categoryService = categoryService;
            _settingService = settingService;
            _socialService = socialService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetHighlightedProjects();
            var categories = await _categoryService.GetHighlightedCategoriesAsync();
            var page = await _pageService.GetByUrlAsync("anasayfa");

            if(page is null)
                return NotFound();

            var vm = new PageViewModel
            {
                Page = page,
                Projects = projects,
                Categories = categories
            };

            return View(vm);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
