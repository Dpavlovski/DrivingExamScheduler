using DrivingExamScheduler.Domain.Models.Domain;

namespace DrivingExamScheduler.Service.Interface
{
    public interface ITimeSlotService
    {
        public List<TimeSlot> ListAllTimeSlots();

        public TimeSlot GetTimeSlot(Guid? id);

        public TimeSlot CreateNewTimeSlot(TimeSlot timeSlot);

        public TimeSlot EditTimeSlot(TimeSlot timeSlot);

        public TimeSlot DeleteTimeSlot(Guid id);
    }
}
