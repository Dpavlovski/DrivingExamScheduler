using DrivingExamScheduler.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Domain
{
    public class TimeSlot : BaseEntity
    {
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSlotStatus? Status { get; set; }
        [Required]
        public int MaxCapacity { get; set; }
        public Location? Location { get; set; }
        public Exam? Exam { get; set; }
    }
}
