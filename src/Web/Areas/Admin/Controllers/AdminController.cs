using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _adminService.GetAllAsync();
            return View(admins);
        }

        //[AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var result = await _adminService.LoginAsync(username, password, ip);

            if (result)
            {
                // TODO: Implement proper authentication
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
            return View();
        }

        public IActionResult Logout()
        {
            // TODO: Implement proper logout
            return RedirectToAction("Login");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Core.Entities.Admin admin, string password)
        {
            admin.PasswordHash = HashPassword(password);
            await _adminService.AddAsync(admin);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var admin = await _adminService.GetByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Core.Entities.Admin admin, string password)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(password))
            {
                admin.PasswordHash = HashPassword(password);
            }
            await _adminService.UpdateAsync(admin);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var admin = await _adminService.GetByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var admin = await _adminService.GetByIdAsync(id);
            if (admin != null)
            {
                await _adminService.DeleteAsync(admin);
            }
            return RedirectToAction(nameof(Index));
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
} 