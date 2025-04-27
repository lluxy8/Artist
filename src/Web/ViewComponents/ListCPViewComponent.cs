using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class ListCPViewComponent : ViewComponent
    {
        private readonly PageService _pageService;
        private readonly ProjectService _projectService;
        private readonly CategoryService _categoryService;

        public ListCPViewComponent(
            PageService pageService,
            ProjectService projectService,
            CategoryService categoryService,
            SettingService settingService,
            SocialService socialService)
        {
            _pageService = pageService;
            _projectService = projectService;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string url)
        {
            var page = await _pageService.GetByUrlAsync(url);
            var projects = await _projectService.GetHighlightedProjects();
            var categories = await _categoryService.GetHighlightedCategoriesAsync();
            
            var pageVM = new PageViewModel
            {
                Page = page,
                Projects = projects,
                Categories = categories
            };

            return View("Both", pageVM);

            /*
            if (page.ListProjects)
                return View("Projects", pageVM);
            else if(page.ListCategories)
                return View("Categories", pageVM);
            else if (page.ListProjects && page.ListCategories)
                return View("Both", pageVM);
            
            return View();

            */
        }
    }
}
