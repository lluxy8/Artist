using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly PageService _pageService;
        private readonly CategoryService _categoryService;
        private readonly ProjectService _projectService;
        private readonly SocialService _socialService;

        public HomeController(
            PageService pageService,
            CategoryService categoryService,
            ProjectService projectService,
            SocialService socialService)
        {
            _pageService = pageService;
            _categoryService = categoryService;
            _projectService = projectService;
            _socialService = socialService;
        }
        public IActionResult Index()
        {
            ViewBag.PageCount = _pageService.GetAllAsync().Result.Count;
            ViewBag.ProjectCount = _categoryService.GetAllAsync().Result.Count;
            ViewBag.CategoryCount = _projectService.GetAllAsync().Result.Count;
            ViewBag.SocialCount = _socialService.GetAllAsync().Result.Count;


            return View();
        }
    }
}
