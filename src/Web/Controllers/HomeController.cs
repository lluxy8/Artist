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
            var homePage = await _pageService.GetByUrlAsync("Anasayfa");
            var settings = await _settingService.GetAllAsync();
            var setting = settings.FirstOrDefault();
            var socials = await _socialService.GetAllAsync();
            var highlightedProjects = await _projectService.GetHighlightedProjects();
            var categories = await _categoryService.GetAllAsync();

            ViewBag.Settings = setting;
            ViewBag.Socials = socials;
            ViewBag.HighlightedProjects = highlightedProjects;
            ViewBag.Categories = categories;

            return View(homePage);
        }

        public async Task<IActionResult> Contact()
        {
            var contactPage = await _pageService.GetByUrlAsync("Iletisim");
            var settings = await _settingService.GetAllAsync();
            var setting = settings.FirstOrDefault();
            var socials = await _socialService.GetAllAsync();

            ViewBag.Settings = setting;
            ViewBag.Socials = socials;

            return View(contactPage);
        }

        public async Task<IActionResult> Projects(string categoryUrl = null)
        {
            var settings = await _settingService.GetAllAsync();
            var setting = settings.FirstOrDefault();
            var socials = await _socialService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            
            IEnumerable<Project> projects;
            if (string.IsNullOrEmpty(categoryUrl))
            {
                projects = await _projectService.GetAllAsync();
            }
            else
            {
                var category = categories.FirstOrDefault(c => c.UrlName == categoryUrl);
                if (category == null)
                {
                    return NotFound();
                }
                projects = await _projectService.GetByCategoryIdAsync(category.Id);
            }

            ViewBag.Settings = setting;
            ViewBag.Socials = socials;
            ViewBag.Categories = categories;
            ViewBag.CurrentCategory = categoryUrl;

            return View(projects);
        }

        public async Task<IActionResult> ProjectDetail(Guid id)
        {
            var settings = await _settingService.GetAllAsync();
            var setting = settings.FirstOrDefault();
            var socials = await _socialService.GetAllAsync();
            
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Settings = setting;
            ViewBag.Socials = socials;

            return View(project);
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
