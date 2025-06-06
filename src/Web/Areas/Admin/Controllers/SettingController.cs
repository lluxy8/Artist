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
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
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
                Address = setting.Address,
                PrimaryColor = setting.PrimaryColor ?? "#000",
                SecondaryColor = setting.SecondaryColor ?? "#000",
                ThirdColor = setting.ThirdColor ?? "#000"
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SettingUpdateDto setting)
        {
            if (!ModelState.IsValid) return View(setting);
            await _settingService.UpdateAsync(setting, Request);
            return RedirectToAction(nameof(Index));
        }
    }
} 