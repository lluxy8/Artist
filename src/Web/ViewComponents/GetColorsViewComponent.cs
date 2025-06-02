using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class GetColorsViewComponent : ViewComponent
    {
        private readonly ISettingService _service;

        public GetColorsViewComponent(ISettingService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var colors = (await _service.GetAllAsync()!)
                .Select(x => new ColorViewModel
                {
                    PrimaryColor = x.PrimaryColor,
                    SecondaryColor = x.SecondaryColor,
                    ThirdColor = x.ThirdColor
                })
                .FirstOrDefault();

            return View(colors);
        }
    }
}
