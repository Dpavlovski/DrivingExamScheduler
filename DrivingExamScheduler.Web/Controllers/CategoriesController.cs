using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrivingExamScheduler.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IRequirementService _requirementService;
        private readonly IRequirementsForCategoryService _requirementsForCategoryService;

        public CategoriesController(ICategoryService categoryService, IRequirementService requirementService, IRequirementsForCategoryService requirementsForCategoryService)
        {
            _categoryService = categoryService;
            _requirementService = requirementService;
            _requirementsForCategoryService = requirementsForCategoryService;
        }


        public IActionResult Index()
        {
            ViewData["Requirements"] = _requirementsForCategoryService.ListAllRequirementsForCategories();
            return View(_categoryService.ListAllCategories());

        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            ViewData["Requirements"] = _requirementService.ListAllRequirements();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category, Guid[] selectedRequirements)
        {

            _categoryService.CreateNewCategory(category, selectedRequirements);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["Requirements"] = _requirementService.ListAllRequirements();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Category category, Guid[] selectedRequirements)
        {
            if (id != category.Id)
            {
                return NotFound();
            }


            try
            {
                _categoryService.EditCategory(category, selectedRequirements);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(Guid id)
        {
            return _categoryService.GetCategory(id) != null ? true : false;
        }
    }
}
