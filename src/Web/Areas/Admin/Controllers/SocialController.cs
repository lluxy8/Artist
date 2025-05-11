using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Core.DTOs;
using Core.Interfaces.Service;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SocialController : Controller
    {
        private readonly ISocialService _socialService;

        public SocialController(ISocialService socialService)
        {
            _socialService = socialService;
        }

        public async Task<IActionResult> Index()
        {
            var socials = await _socialService.GetAllAsync();
            return View(socials);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SocialCreateDto social)
        {
            if (ModelState.IsValid)
            {
                await _socialService.AddAsync(social, Request);
                return RedirectToAction(nameof(Index));
            }


            return View(social);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var social = await _socialService.GetByIdAsync(id);
            if (social == null)
            {
                return NotFound();
            }

            var dto = new SocialUpdateDto
            {
                Id = social.Id,
                IconUrl = social.IconUrl,
                Name = social.Name,
                Url = social.Url
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, SocialUpdateDto social)
        {
            if (id != social.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {

                await _socialService.UpdateAsync(social, Request);
                return RedirectToAction(nameof(Index));
            }

            return View(social);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var social = await _socialService.GetByIdAsync(id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var social = await _socialService.GetByIdAsync(id);
            if (social != null)
            {
                await _socialService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 