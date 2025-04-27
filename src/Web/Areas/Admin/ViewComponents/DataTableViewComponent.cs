using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.Models;

namespace Web.Areas.Admin.ViewComponents
{
    public class DataTableViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string tableId, List<string> columns, bool isOrderable = true)
        {
            var model = new DataTableViewModel
            {
                TableId = tableId,
                Columns = columns,
                IsOrderable = isOrderable
            };
            return View(model);
        }
    }
} 