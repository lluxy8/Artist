using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Core.DTOs;

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
                await Task.Delay(500);
            }
            
            var dto = new SettingUpdateDto
            {
                Id = setting.Id,                
                LogoUrl = setting.LogoUrl,
                CompanyName = setting.CompanyName,
                DoneCustomerCount = setting.DoneCustomerCount,
                DoneProjectsCount = setting.DoneProjectsCount,
                AddressGoogleMaps = setting.AddressGoogleMaps,
                ExperienceYear = setting.ExperienceYear,
                PhoneNumber = setting.PhoneNumber,
                Email = setting.Email,
                Address = setting.Address
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SettingUpdateDto setting)
        {
            if (ModelState.IsValid)
            {
                await _settingService.UpdateAsync(setting, Request);
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }
    }
} 