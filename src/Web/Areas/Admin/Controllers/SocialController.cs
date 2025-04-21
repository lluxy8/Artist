using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class SocialController : Controller
    {
        private readonly SocialService _socialService;

        public SocialController(SocialService socialService)
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
        public async Task<IActionResult> Create(Social social)
        {
            if (ModelState.IsValid)
            {
                await _socialService.AddAsync(social);
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
            return View(social);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Social social)
        {
            if (id != social.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _socialService.UpdateAsync(social);
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
                await _socialService.DeleteAsync(social);
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 