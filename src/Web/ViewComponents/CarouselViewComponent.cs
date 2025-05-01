using Application.Services;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class CarouselViewComponent : ViewComponent
    {
        private readonly IPageService _page;
        public CarouselViewComponent(IPageService page)
        {
            _page = page;
        }

        public async Task<IViewComponentResult> InvokeAsync(string url)
        {
            var carousels = (await _page.GetByUrlAsync(url))?
                .PageContent?.PageCarousels
                .Select(x => new CarouselViewModel
                {
                    ImageUrl = x.ImageUrl,
                    Text1 = x.Text1,
                    Text2 = x.Text2,
                    Text3 = x.Text3
                })
                .ToList();

            return View(carousels);
        }
    }
}
