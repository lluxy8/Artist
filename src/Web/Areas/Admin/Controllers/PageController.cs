using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Core.DTOs;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class PageController : Controller
    {
        private readonly PageService _pageService;
        private readonly PageContentService _pageContentService;
        private readonly PageCarouselService _pageCarouselService;

        public PageController(
            PageService pageService,
            PageContentService pageContentService,
            PageCarouselService pageCarouselService)
        {
            _pageService = pageService;
            _pageContentService = pageContentService;
            _pageCarouselService = pageCarouselService;
        }

        public async Task<IActionResult> Index()
        {
            var pages = await _pageService.GetAllAsync();
            return View(pages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Page page)
        {
            await _pageService.AddAsync(page);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var page = await _pageService.GetByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Page page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            await _pageService.UpdateAsync(page);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var page = await _pageService.GetByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var page = await _pageService.GetByIdAsync(id);
            if (page != null)
            {
                await _pageService.DeleteAsync(page);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Content(Guid id)
        {
            var pageContent = await _pageContentService.GetAllAsync(true);
            var content = pageContent.FirstOrDefault(x => x.PageId == id);
            ViewBag.PageId = id;

            if (content == null)
            {
                content = new PageContent { PageId = id };
                await _pageContentService.AddAsync(content);
            }


            return View(content);
        }

        [HttpPost]
        public async Task<IActionResult> Content(PageContent content)
        {
            await _pageContentService.UpdateAsync(content);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Carousel(Guid PageId, Guid pageContentId)
        {
            var carousels = await _pageCarouselService.GetAllAsync();
            ViewBag.PageId = PageId;
            ViewBag.pageContentId = pageContentId;
            return View(carousels.Where(x => x.PageContentId == pageContentId).ToList());
        }

        public IActionResult CreateCarousel(Guid PageId, Guid pageContentId)
        {
            var carousel = new PageCarouselCreateDto { PageContentId = pageContentId };
            ViewBag.PageId = PageId;
            return View(carousel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarousel(PageCarouselCreateDto carousel)
        {
            await _pageCarouselService.AddAsync(carousel, Request);
            return RedirectToAction(nameof(Carousel), new { pageContentId = carousel.PageContentId });
        }

        public async Task<IActionResult> EditCarousel(Guid PageId, Guid id)
        {
            var carousel = await _pageCarouselService.GetByIdAsync(id);
            if (carousel == null)
                return NotFound();

            ViewBag.PageId = PageId;

            var dto = new PageCarouselUpdateDto
            {
                Id = carousel.Id,
                PageContentId = carousel.PageContentId,
                ImageUrl = carousel.ImageUrl,
                Text1 = carousel.Text1,
                Text2 = carousel.Text2,
                Text3 = carousel.Text3,
                PageContent = carousel.PageContent,
                CreateDate = carousel.CreateDate,
                UpdateDate = carousel.UpdateDate
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditCarousel(Guid id, PageCarouselUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            await _pageCarouselService.UpdateAsync(dto, Request);
            return RedirectToAction(nameof(Carousel), new { pageContentId = dto.PageContentId });
        }

        public async Task<IActionResult> DeleteCarousel(Guid pageid, Guid id)
        {
            var carousel = await _pageCarouselService.GetByIdAsync(id);
            if (carousel == null)
            {
                return NotFound();
            }
            ViewBag.PageId = pageid;
            return View(carousel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCarouselConfirmed(Guid id)
        {
            var carousel = await _pageCarouselService.GetByIdAsync(id);
            if (carousel != null)
            {
                var pageContentId = carousel.PageContentId;
                await _pageCarouselService.DeleteAsync(carousel);
                return RedirectToAction(nameof(Carousel), new { pageContentId });
            }
            return NotFound();
        }
    }
} 