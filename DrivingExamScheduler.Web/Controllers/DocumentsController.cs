using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Security.Claims;

namespace DrivingExamScheduler.Web.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IUserRepository _userRepository;
        public DocumentsController(IDocumentService documentService, IUserRepository userRepository)
        {
            _documentService = documentService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid DocumentTypeId, IFormFile file, DateTime ExpirationDate)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _documentService.CreateNewDocument(userId, DocumentTypeId, file, ExpirationDate);
            return RedirectToAction("CandidateProfile", "Account");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _documentService.DeleteDocument(id);
            return RedirectToAction("CandidateProfile", "Account");
        }

        public IActionResult DownloadAllCandidateDocuments()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepository.Get(userId);
            var userFolderPath = $"{Directory.GetCurrentDirectory()}\\files\\{user.UserName}";

            if (!Directory.Exists(userFolderPath))
            {
                return NotFound("User folder not found");
            }

            var zipFilePath = Path.Combine(Path.GetTempPath(), "CandidateDocuments.zip");

            // Create a ZIP archive of the user's folder
            ZipFile.CreateFromDirectory(userFolderPath, zipFilePath);

            // Define the content type for the ZIP file
            var contentType = "application/zip";

            // Return the ZIP file for download
            return File(System.IO.File.OpenRead(zipFilePath), contentType, "CandidateDocuments.zip");
        }
    }

}
