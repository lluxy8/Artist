using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class GaleriController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;   

        public GaleriController(IProjectService projectService, ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _projectService = projectService;
        }

        [Route("Galeri/{kategori?}/{altkategori?}")]
        public async Task<IActionResult> Index(string kategori, string altkategori)
        {
            var vm = new GaleriPageViewModel
            {
                Categories = await _categoryService.GetAllAsync(true)
            };

            if (string.IsNullOrEmpty(kategori))
            {
                vm.Projects = await _projectService.GetAllAsync(true) ?? [];
            }
            else
            {
                var category = await _categoryService.GetByUrlAsync(kategori);

                if (category is null)
                {
                    return NotFound();
                }

                vm.Selected = category;

                if (string.IsNullOrEmpty(altkategori)) return View(vm);

                var selectedSub = await _subCategoryService.GetByUrlAsync(altkategori);

                if (selectedSub != null)
                {
                    if (selectedSub.UrlName == $"{category.UrlName}-kategorisiz")
                    {
                        vm.Projects = await _projectService.GetByCategoryIdAsync(selectedSub.CategoryId) ?? [];
                        vm.SelectedSub = selectedSub;
                        return View(vm);
                    }

                    vm.Projects = await _projectService.GetBySubCategoryIdAsync(selectedSub.Id) ?? [];
                    vm.SelectedSub = selectedSub;
                }
                else
                {
                    return NotFound();
                }

            }

            return View(vm);
        }

        [Route("Galeri/Detay/{url}")]
        public async Task<IActionResult> Detay(string url)
        {
            var project = await _projectService.GetByUrlAsync(url);

            if (project is null)
                return NotFound();

            var subCategory = await _subCategoryService.GetByIdAsync(project.SubCategoryId, true);

            var vm = new DetailViewModel
            {
                CategoryName = subCategory.Category.DisplayName,
                SubCategoryName = project.SubCategory.DisplayName,
                ProjectName = project.DisplayName,
                Description = project.Description,
                CreateDate = project.CreateDate,
                Reference = project.Refference,
                Images = project.Images.Select(x => x.Url).ToList()
            };

            return View(vm);
        }
    }
}
