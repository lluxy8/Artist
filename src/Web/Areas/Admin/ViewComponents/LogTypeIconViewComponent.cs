using Core.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.ViewComponents
{
    public class LogTypeIconViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(LogType type)
        {
            return View("Default", type);
        }
    }
}
