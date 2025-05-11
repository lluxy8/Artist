using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Application.Services;
using Core.Entities;
using Core.Interfaces.Service;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
