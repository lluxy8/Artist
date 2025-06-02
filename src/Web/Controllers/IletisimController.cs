using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class IletisimController : Controller
    {
        private readonly IPageService _pageService;
        private readonly ISettingService _settingService;

        public IletisimController(ISettingService settingService, IPageService pageService)
        {
            _pageService = pageService;
            _settingService = settingService;   
        }

        public async Task<IActionResult> Index()
        {
            var page = await _pageService.GetByUrlAsync("ILetisim");
            if (page is null)
                return NotFound();

            var settings = (await _settingService.GetAllAsync()).FirstOrDefault();  

            var vm = new IletisimPageViewModel
            {
                Address = settings?.Address ?? string.Empty,
                PageSections = page.PageContent?.PageSections ?? [],
                AddressGoogleMaps = settings?.AddressGoogleMaps ?? string.Empty,
                PhoneNumber = settings?.PhoneNumber ?? string.Empty,
                Email = settings?.Email ?? string.Empty
            };  

            return View(vm);
        }
    }
}
