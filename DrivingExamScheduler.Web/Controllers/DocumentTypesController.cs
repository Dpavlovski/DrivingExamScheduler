using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrivingExamScheduler.Web.Controllers
{
    public class DocumentTypesController : Controller
    {
        private readonly IDocumentTypeService _documentTypeService;

        public DocumentTypesController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        public IActionResult Index()
        {
            return View(_documentTypeService.ListAllDocumentTypes());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = _documentTypeService.GetDocumentType(id);

            return View(documentType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DocumentType documentType)
        {
            _documentTypeService.CreateNewDocumentType(documentType);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = _documentTypeService.GetDocumentType(id);
            return View(documentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, DocumentType documentType)
        {
            if (id != documentType.Id)
            {
                return NotFound();
            }


            try
            {
                _documentTypeService.EditDocumentType(documentType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentTypeExists(documentType.Id))
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

            var documentType = _documentTypeService.GetDocumentType(id);
            return View(documentType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var documentType = _documentTypeService.DeleteDocumentType(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentTypeExists(Guid id)
        {
            return _documentTypeService.GetDocumentType(id) != null ? true : false;
        }

    }
}
