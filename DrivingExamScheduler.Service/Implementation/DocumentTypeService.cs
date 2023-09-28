using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;

namespace DrivingExamScheduler.Service.Implementation
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IRepository<DocumentType> _documentTypeRepository;

        public DocumentTypeService(IRepository<DocumentType> documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }
        public DocumentType CreateNewDocumentType(DocumentType documentType)
        {
            documentType.Id = Guid.NewGuid();
            return _documentTypeRepository.Insert(documentType);
        }

        public DocumentType DeleteDocumentType(Guid id)
        {
            var documentType = _documentTypeRepository.Get(id);
            return _documentTypeRepository.Delete(documentType);
        }

        public DocumentType EditDocumentType(DocumentType documentType)
        {

            return _documentTypeRepository.Update(documentType);
        }

        public DocumentType GetDocumentType(Guid? id)
        {
            return _documentTypeRepository.Get(id);
        }

        public List<DocumentType> ListAllDocumentTypes()
        {
            return _documentTypeRepository.GetAll().ToList();
        }

    }
}
