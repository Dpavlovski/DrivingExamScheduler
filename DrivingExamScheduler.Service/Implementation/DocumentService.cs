using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace DrivingExamScheduler.Service.Implementation
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;
        private readonly IUserRepository _userRepository;

        public DocumentService(IRepository<Document> documentRepository, IUserRepository userRepository)
        {
            _documentRepository = documentRepository;
            _userRepository = userRepository;
        }
        public Document CreateNewDocument(string userId, Guid DocumentTypeId, IFormFile file, DateTime ExpirationDate)
        {
            SaveDocument(userId, file);
            var candidate = _userRepository.Get(userId);

            Document document = new()
            {
                Id = Guid.NewGuid(),
                CandidateId = candidate.Id,
                DocumentTypeId = DocumentTypeId,
                ExpirationDate = ExpirationDate,
                FileName = file.FileName,
                FilePath = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}"
            };

            return _documentRepository.Insert(document);
        }

        public Document DeleteDocument(Guid id)
        {
            var document = _documentRepository.Get(id, z => z.Candidate, z => z.DocumentType);
            File.Delete(document.FilePath);
            return _documentRepository.Delete(document);
        }

        public List<Document> GetCandidateDocuments(string userId)
        {
            return _documentRepository.GetAll().Where(z => z.CandidateId == userId).ToList();
        }

        public Document GetDocument(Guid? id)
        {
            return _documentRepository.Get(id);
        }


        private void SaveDocument(string userId, IFormFile file)
        {
            var username = _userRepository.Get(userId).UserName;

            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}\\files\\{username}"))
            {
                Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\files\\{username}");
            }

            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{username}\\{file.FileName}";
            using (FileStream fileStream = File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
        }
    }
}
