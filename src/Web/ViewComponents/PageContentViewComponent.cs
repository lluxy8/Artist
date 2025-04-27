using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class PageContentViewComponent : ViewComponent
    {
        private readonly PageService _pageService;

        public PageContentViewComponent(PageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string url)
        {
            var page = await _pageService.GetByUrlAsync(url);


            var vm = new PageContentViewModel
            {
                ImageUrl = page.PageContent?.ImageUrl,
                Text1 = page.PageContent?.Text1,
                Text2 = page.PageContent?.Text2,
                Text3 = page.PageContent?.Text3
            };
            


            return View(vm);
        }
    }
}
