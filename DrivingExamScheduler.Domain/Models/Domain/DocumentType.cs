
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Domain
{
    public class DocumentType : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public virtual ICollection<Requirement>? Requirements { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }

    }
}
