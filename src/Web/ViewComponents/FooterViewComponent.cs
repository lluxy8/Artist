using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly SettingService _settingService;
        private readonly SocialService _socialService;
        private readonly CategoryService _categoryService;
        private readonly ProjectService _projectService;

        public FooterViewComponent(
            SettingService settingService, 
            SocialService socialService,
            CategoryService categoryService,
            ProjectService projectService)
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
