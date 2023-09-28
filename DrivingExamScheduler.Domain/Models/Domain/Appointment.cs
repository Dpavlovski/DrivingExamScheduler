using DrivingExamScheduler.Domain.Models.Enum;
using DrivingExamScheduler.Domain.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Domain
{
    public class Appointment : BaseEntity
    {
        [Required]
        public string? CandidateId { get; set; }
        [Required]
        public Guid ExamId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public MeetsRequirementsStatus MeetsRequirements { get; set; }
        public string? RejectionReason { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        [Required]
        public AppointmentStatus Status { get; set; }
        public Exam? Exam { get; set; }
        public Candidate? Candidate { get; set; }

    }
}
