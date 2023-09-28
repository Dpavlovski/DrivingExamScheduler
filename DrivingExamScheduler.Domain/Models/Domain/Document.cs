using DrivingExamScheduler.Domain.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Domain
{
    public class Document : BaseEntity
    {
        [Required]
        public Guid DocumentTypeId { get; set; }
        [Required]
        public string? CandidateId { get; set; }
        [Required]
        public bool IsValid { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string? FileName { get; set; }
        [Required]
        public string? FilePath { get; set; }
        public DocumentType? DocumentType { get; set; }
        public Candidate? Candidate { get; set; }
    }
}
