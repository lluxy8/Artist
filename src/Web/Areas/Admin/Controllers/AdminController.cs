using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAuthenticationManager _authenticationManager;

        public AdminController(IAdminService adminService, IAuthenticationManager authenticationManager)
        {
            _adminService = adminService;
            _authenticationManager = authenticationManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _adminService.GetAllAsync();
            return View(admins);
        }

        [Route("Admin/Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("login")]
        [Route("Admin/Login")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto, string? returnUrl = null)
        {
            var result = await _adminService.LoginAsync(dto);

            if (ModelState.IsValid && result)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }


            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
            return View();
        }

        public IActionResult Logout()
        {
            _authenticationManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(AdminCreateDto dto)
        {
            if(ModelState.IsValid)
            {
                dto.IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
                await _adminService.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var admin = await _adminService.GetByIdAsync(id);

            var dto = new AdminUpdateDto
            {
                Id = admin.Id,
                Name = admin.Name,
                Password = string.Empty,
            };

            if (admin == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AdminUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await _adminService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var admin = await _adminService.GetByIdAsync(id);
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _adminService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}