using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Domain.Models.Enum;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;

namespace DrivingExamScheduler.Service.Implementation
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly IRepository<TimeSlot> _timeSlotRepository;

        public TimeSlotService(IRepository<TimeSlot> timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public TimeSlot CreateNewTimeSlot(TimeSlot timeSlot)
        {
            timeSlot.Id = Guid.NewGuid();
            timeSlot.Status = TimeSlotStatus.NotFull;
            return _timeSlotRepository.Insert(timeSlot);
        }

        public TimeSlot DeleteTimeSlot(Guid id)
        {
            var timeSlot = GetTimeSlot(id);
            return _timeSlotRepository.Delete(timeSlot);
        }

        public TimeSlot EditTimeSlot(TimeSlot timeSlot)
        {

            return _timeSlotRepository.Update(timeSlot);

        }

        public TimeSlot GetTimeSlot(Guid? id)
        {
            return _timeSlotRepository.Get(id, z => z.Location);
        }

        public List<TimeSlot> ListAllTimeSlots()
        {
            return _timeSlotRepository.GetAll(z => z.Location).ToList();
        }
    }
}
