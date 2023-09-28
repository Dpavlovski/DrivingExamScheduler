using DrivingExamScheduler.Domain.Models.Domain;

namespace DrivingExamScheduler.Service.Interface
{
    public interface IDocumentTypeService
    {
        public List<DocumentType> ListAllDocumentTypes();

        public DocumentType GetDocumentType(Guid? id);

        public DocumentType CreateNewDocumentType(DocumentType documentType);

        public DocumentType EditDocumentType(DocumentType documentType);

        public DocumentType DeleteDocumentType(Guid id);
    }
}
