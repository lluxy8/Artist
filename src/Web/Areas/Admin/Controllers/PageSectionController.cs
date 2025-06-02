using Application.Services;
using Core.Common.Models;
using Core.DTOs;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PageSectionController : Controller
    {
        private readonly IPageSectionService _pageSectionService;
        private readonly IPageContentService _pageContentService;

        public PageSectionController(IPageSectionService pageSectionService, IPageContentService pageContentService)
        {
            _pageSectionService = pageSectionService;
            _pageContentService = pageContentService;
        }

        public async Task<IActionResult> Index(Guid pageId, Guid pcId)
        {
            var pc = await _pageContentService.GetByIdAsync(pcId);

            if (pc is null)
                return NotFound();

            var categories = await _pageSectionService.GetByPageContentAsync(pcId);

            ViewBag.pageId = pageId;
            ViewBag.pcId = pcId;
            
            return View(categories);
        }

        public async Task<IActionResult> Create(Guid pcId, Guid pageId)
        {
            var pc = await _pageContentService.GetByIdAsync(pcId);

            if (pc is null)
                return NotFound();

            ViewBag.pcId = pcId;
            ViewBag.pageId = pageId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid returnPageId,  PageSectionCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _pageSectionService.AddAsync(dto, Request);
            return RedirectToAction(nameof(Index), new {pcId = dto.PageContentId, pageId = returnPageId });

        }

        public async Task<IActionResult> Edit(Guid pageId, Guid id)
        {
            var ps = await _pageSectionService.GetByIdAsync(id);

            if (ps is null)
            {
                return NotFound();
            }

            var dto = new PageSectionUpdateDto
            {
                Id = ps.Id,
                Text1 = ps.Text1,
                Text2 = ps.Text2,
                Text3 = ps.Text3,
                PageContentId = ps.PageContentId,
                ImageUrl = ps.ImageUrl,
                Order = ps.DisplayOrder,
                ImagePosition = ps.ImagePosition
            };

            ViewBag.PageId = pageId;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCarouselOrders([FromBody] List<DisplayOrderModel> items)
        {
            await _pageSectionService.UpdateOrders(items);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid pageId, PageSectionUpdateDto dto)
        {

            if (!ModelState.IsValid) return View(dto);

            await _pageSectionService.UpdateAsync(dto, Request);
            return RedirectToAction(nameof(Index), new { pageId = pageId, pcId = dto.PageContentId });

        }

        public async Task<IActionResult> Delete(Guid pageId, Guid id)
        {
            var ps = await _pageSectionService.GetByIdAsync(id);
            if (ps == null)
            {
                return NotFound();
            }

            ViewBag.PageId = pageId;
            return View(ps);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid pageId, Guid id)
        {
            var ps = await _pageSectionService.GetByIdAsync(id);

            if (ps == null) return BadRequest();

            await _pageSectionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { pcId = ps.PageContentId, pageId = pageId });

        }
    }
}
