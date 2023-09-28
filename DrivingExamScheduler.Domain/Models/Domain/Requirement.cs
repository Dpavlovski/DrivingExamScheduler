using DrivingExamScheduler.Domain.Models.Enum;
using DrivingExamScheduler.Domain.Models.Realation;
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Domain
{
    public class Requirement : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public RequirementType Type { get; set; }
        public int? RequiredAge { get; set; }
        public Guid? RequiredCategoryId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public int? YearsFromLastPass { get; set; }
        public Category? RequiredCategory { get; set; }
        public DocumentType? DocumentType { get; set; }
        public virtual ICollection<RequirementForCategory>? RequirementsForCategory { get; set; }
    }
}
