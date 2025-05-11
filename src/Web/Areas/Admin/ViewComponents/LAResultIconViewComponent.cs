using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.ViewComponents
{
    public class LAResultIconViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(bool success)
        {
            return View("Default", success);
        }
    }
}
