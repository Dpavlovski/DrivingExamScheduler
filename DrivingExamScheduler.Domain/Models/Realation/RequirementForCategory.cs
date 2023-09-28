using DrivingExamScheduler.Domain.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Realation
{
    public class RequirementForCategory : BaseEntity
    {
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid RequirementId { get; set; }
        public Category? Category { get; set; }
        public Requirement? Requirement { get; set; }
    }
}
