using DrivingExamScheduler.Domain.Models.Identity;
using DrivingExamScheduler.Domain.Models.Realation;
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Domain
{
    public class Category : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public virtual ICollection<Candidate>? Candidates { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
        public virtual ICollection<Requirement>? Requirements { get; set; }
        public virtual ICollection<RequirementForCategory>? RequirementsForCategory { get; set; }

    }
}
