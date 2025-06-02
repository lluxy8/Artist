using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class SayfalarController : Controller
    {
        private readonly IPageService _pageService;

        public SayfalarController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [Route("sayfalar/{url}")]
        public async Task<IActionResult> Index(string url)
        {
            var page = await _pageService.GetByUrlAsync(url);

            if(page is null)
                return NotFound();

            ViewBag.Url = url;
            return View();
        }
    }
}
