using DrivingExamScheduler.Domain.Models.Domain;
using Microsoft.AspNetCore.Http;

namespace DrivingExamScheduler.Service.Interface
{
    public interface IDocumentService
    {
        public List<Document> GetCandidateDocuments(string userId);

        public Document GetDocument(Guid? id);

        public Document CreateNewDocument(string userId, Guid DocumentTypeId, IFormFile file, DateTime ExpirationDate);

        public Document DeleteDocument(Guid id);
    }
}
