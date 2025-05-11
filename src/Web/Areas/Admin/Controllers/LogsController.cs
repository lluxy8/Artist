using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LogsController : Controller
    {
        private readonly ILogService _logService;
        private readonly ILoginAttemptService _loginAttemptService;

        public LogsController(ILogService logService, ILoginAttemptService loginAttemptService)
        {
            _logService = logService;
            _loginAttemptService = loginAttemptService;
        }
        public async Task<IActionResult> Index()
        {
            var logs = await _logService.GetAllAsync();
            return View(logs);
        }

        public async Task<IActionResult> LoginAttempts()
        {
            var logs = await _loginAttemptService.GetAllAsync();
            return View(logs);
        }
    }
}
