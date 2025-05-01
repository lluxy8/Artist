using Application.Services;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly ISocialService _socialService;
        private readonly ICategoryService _categoryService;
        private readonly IProjectService _projectService;

        public FooterViewComponent(
            ISettingService settingService, 
            ISocialService socialService,
            ICategoryService categoryService,
            IProjectService projectService)
        {
            _settingService = settingService;
            _socialService = socialService;
            _categoryService = categoryService;
            _projectService = projectService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new FooterViewModel
            {
                Settings = (await _settingService.GetAllAsync()).FirstOrDefault(),
                Socials = await _socialService.GetAllAsync(),
                Categories = await _categoryService.TakeAsync(5),
                Projects = await _projectService.TakeAsync(5)
            };


            return View(vm);
        }

    }
}
