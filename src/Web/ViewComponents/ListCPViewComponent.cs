using Application.Services;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
    public class ListCPViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;
        private readonly IProjectService _projectService;
        private readonly ICategoryService _categoryService;

        public ListCPViewComponent(
            IPageService pageService,
            IProjectService projectService,
            ICategoryService categoryService)
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
            
            var pageVm = new PageViewModel
            {
                Url = page?.UrlName ?? string.Empty,
                PageSections = page?.PageContent?.PageSections ?? [],
                Projects = projects,
                Categories = categories
            };



            switch (page.ListProjects)
            {
                case true when page.ListCategories:
                    return View("Both", pageVm);
                case true:
                    return View("Projects", pageVm);
                default:
                {
                    if(page.ListCategories)
                        return View("Categories", pageVm);
                    break;
                }
            }

            return View();
        }
    }
}
