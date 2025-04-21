using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class SettingController : Controller
    {
        private readonly SettingService _settingService;

        public SettingController(SettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllAsync();
            var setting = settings.FirstOrDefault();
            
            if (setting == null)
            {
                setting = new Setting();
                await _settingService.AddAsync(setting);
            }
            
            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Setting setting)
        {
            if (ModelState.IsValid)
            {
                await _settingService.UpdateAsync(setting);
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }
    }
} 