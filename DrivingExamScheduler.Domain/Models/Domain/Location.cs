using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Domain
{
    public class Location : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public virtual ICollection<TimeSlot>? TimeSlots { get; set; }
    }
}
