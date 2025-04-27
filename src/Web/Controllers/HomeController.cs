using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Application.Services;
using Core.Entities;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PageService _pageService;
        private readonly ProjectService _projectService;
        private readonly CategoryService _categoryService;
        private readonly SettingService _settingService;
        private readonly SocialService _socialService;

        public HomeController(
            ILogger<HomeController> logger,
            PageService pageService,
            ProjectService projectService,
            CategoryService categoryService,
            SettingService settingService,
            SocialService socialService)
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

            //var settings = (await _settingService.GetAllAsync()).FirstOrDefault();

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
