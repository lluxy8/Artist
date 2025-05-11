using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HakkimizdaController : Controller
    {
        private readonly IPageService _pageService;
        private readonly ISettingService _settingService;

        public HakkimizdaController(
            IPageService pageService,
            ISettingService settingService)
        {
            _pageService = pageService;
            _settingService = settingService;
        }


        public async Task<IActionResult> Index()
        {
            var page = await _pageService.GetByUrlAsync("hakkimizda");
            var settings = (await _settingService.GetAllAsync()).FirstOrDefault();

            if (page is null)
                return NotFound();

            var vm = new AboutPageViewModel
            {
                Page = page,
                DoneCustomerCount = settings.DoneCustomerCount,
                DoneProjectsCount = settings.DoneProjectsCount,
                ExperienceYear = settings.ExperienceYear
            };
            return View(vm);
        }
    }
}
