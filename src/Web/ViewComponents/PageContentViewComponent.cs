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
            var vm = (await _pageService.GetByUrlAsync(url))?
                .PageContent?.PageSections
                .Select(x => new PageContentViewModel
                {
                    ImageUrl = x.ImageUrl,
                    Text1 = x.Text1,
                    Text2 = x.Text2,
                    Text3 = x.Text3,
                    DisplayOrder = x.DisplayOrder,
                    ImagePosition = x.ImagePosition
                })
                .OrderBy(x => x.DisplayOrder)
                .ToList() ?? [];

            return View(vm);
        }
    }
}
