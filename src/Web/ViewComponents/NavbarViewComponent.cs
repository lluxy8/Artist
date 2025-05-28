using Application.Services;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IPageService _page;
        private readonly ISettingService _setting;
        public NavbarViewComponent(IPageService page, ISettingService setting)
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

            ViewBag.CName = (await _setting.GetAllAsync()).FirstOrDefault()?.CompanyName;

            return View(pages);
        }
    }
}
