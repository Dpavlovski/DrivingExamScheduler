using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Domain.Models.Enum;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DrivingExamScheduler.Web.Controllers
{
    public class RequirementsController : Controller
    {
        private readonly IRequirementService _requirementService;
        private readonly IDocumentTypeService _documentTypeService;
        private readonly ICategoryService _categoryService;

        public RequirementsController(IRequirementService requirementService, IDocumentTypeService documentTypeService, ICategoryService categoryService)
        {
            _requirementService = requirementService;
            _documentTypeService = documentTypeService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_requirementService.ListAllRequirements());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirement = _requirementService.GetRequirement(id);
            return View(requirement);
        }

        public IActionResult Create()
        {
            ViewData["RequirementTypes"] = new SelectList(Enum.GetValues(typeof(RequirementType)));
            ViewData["DocumentTypeId"] = new SelectList(_documentTypeService.ListAllDocumentTypes(), "Id", "Name");
            ViewData["RequiredCategoryId"] = new SelectList(_categoryService.ListAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Requirement requirement)
        {
            _requirementService.CreateNewRequirement(requirement);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirement = _requirementService.GetRequirement(id);
            ViewData["RequirementTypes"] = new SelectList(Enum.GetValues(typeof(RequirementType)));
            ViewData["DocumentTypeId"] = new SelectList(_requirementService.ListAllRequirements(), "Id", "Name", requirement.DocumentTypeId);
            ViewData["RequiredCategoryId"] = new SelectList(_categoryService.ListAllCategories(), "Id", "Name", requirement.RequiredCategoryId);
            return View(requirement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Requirement requirement)
        {
            if (id != requirement.Id)
            {
                return NotFound();
            }


            try
            {
                _requirementService.EditRequirement(requirement);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementExists(requirement.Id))
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

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirement = _requirementService.GetRequirement(id);

            return View(requirement);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var requirement = _requirementService.DeleteRequirement(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementExists(Guid id)
        {
            return _requirementService.GetRequirement(id) != null ? true : false;
        }
    }
}
