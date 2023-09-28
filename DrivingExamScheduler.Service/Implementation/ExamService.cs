using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;

namespace DrivingExamScheduler.Service.Implementation
{
    public class ExamService : IExamService
    {
        private readonly IRepository<Exam> _examRepository;
        private readonly ITimeSlotService _timeSlotService;

        public ExamService(IRepository<Exam> examRepository, ITimeSlotService timeSlotService)
        {
            _examRepository = examRepository;
            _timeSlotService = timeSlotService;
        }

        public Exam CreateNewExam(Exam exam)
        {
            exam.Id = Guid.NewGuid();
            exam.NumberOfCandidates = 0;
            exam.TimeSlot = _timeSlotService.GetTimeSlot(exam.TimeSlotId);
            return _examRepository.Insert(exam);
        }

        public Exam DeleteExam(Guid id)
        {
            var exam = _examRepository.Get(id);
            return _examRepository.Delete(exam);
        }

        public Exam EditExam(Exam exam)
        {
            return _examRepository.Update(exam);
        }

        public Exam GetExam(Guid? id)
        {
            return _examRepository.Get(id, z => z.TimeSlot, z => z.TimeSlot.Location, z => z.Category);
        }

        public List<Exam> ListAllExams()
        {
            return _examRepository.GetAll(z => z.TimeSlot, z => z.TimeSlot.Location, z => z.Category).ToList();
        }
    }
}
