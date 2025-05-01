using Application.Services;
using Core.Common.Enums;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPageService _pageService;
        private readonly ICategoryService _categoryService;
        private readonly IProjectService _projectService;
        private readonly ISocialService _socialService;
        private readonly ILogService _logService;
        private readonly ILoginAttemptService _loginAttemptService;

        public HomeController(
            IPageService pageService,
            ICategoryService categoryService,
            IProjectService projectService,
            ISocialService socialService,
            ILogService logService,
            ILoginAttemptService loginAttemptService)
        {
            _pageService = pageService;
            _categoryService = categoryService;
            _projectService = projectService;
            _socialService = socialService;
            _logService = logService;
            _loginAttemptService = loginAttemptService;
        }
        public async Task<IActionResult> Index()
        {

            var vm = new DashboardViewModel
            {
                PageCount = (await _pageService.GetAllAsync()).Count,
                ProjectCount = (await _categoryService.GetAllAsync()).Count,
                CategoryCount = (await _projectService.GetAllAsync()).Count,
                SocialCount = (await _socialService.GetAllAsync()).Count,
                LogCountToday = (await _logService.GetByDateFilterAsync
                    (predefinedRange: DateFilter.Last24Hours)).Count,
                LoginCountToday = (await _loginAttemptService.GetByDateFilterAsync
                    (predefinedRange: DateFilter.Last24Hours)).Count,
                Logs = await _logService.TakeAsync(5, x => x.CreateDate),
                LoginAttempts = await _loginAttemptService.TakeAsync(5, x => x.CreateDate)
            };


            return View(vm);
        }
    }
}
