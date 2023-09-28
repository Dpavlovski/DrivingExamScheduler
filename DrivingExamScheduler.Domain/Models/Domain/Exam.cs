using DrivingExamScheduler.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Domain
{
    public class Exam : BaseEntity
    {
        [Required]
        public ExamType ExamType { get; set; }
        [Required]
        public Guid TimeSlotId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public int NumberOfCandidates { get; set; }
        [Required]
        public double Price { get; set; }
        public Category? Category { get; set; }
        public TimeSlot? TimeSlot { get; set; }
        public virtual ICollection<Appointment>? AppointmentsForExam { get; set; }
    }
}
