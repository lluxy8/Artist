using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly PageService _page;
        private readonly SettingService _setting;
        public NavbarViewComponent(PageService page, SettingService setting)
        {
            _page = page;
            _setting = setting;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = (await _page.GetAllAsync()) 
                .Select(static x => new PagesNavViewModel
                {
                    DisplayName = x.DisplayName,
                    UrlName = x.UrlName,
                })
                .OrderBy(x => x.DisplayName)
                .ToList();

            ViewBag.logoUrl = (await _setting.GetAllAsync()).FirstOrDefault()?.LogoUrl;

            return View(pages);
        }
    }
}
