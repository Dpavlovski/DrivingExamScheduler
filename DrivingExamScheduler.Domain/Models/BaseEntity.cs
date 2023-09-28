using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
