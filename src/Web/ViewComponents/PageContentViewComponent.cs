using Application.Services;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class PageContentViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        public PageContentViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string url)
        {
            var page = await _pageService.GetByUrlAsync(url);


            var vm = new PageContentViewModel
            {
                ImageUrl = page?.PageContent?.ImageUrl ?? string.Empty,
                Text1 = page?.PageContent?.Text1 ?? string.Empty,
                Text2 = page?.PageContent?.Text2 ?? string.Empty,
                Text3 = page?.PageContent?.Text3 ?? string.Empty
            };
            


            return View(vm);
        }
    }
}
