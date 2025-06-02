using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class SortableTableViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SortableTableViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
