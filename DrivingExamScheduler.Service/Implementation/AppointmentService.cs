using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Domain.Models.Enum;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;

namespace DrivingExamScheduler.Service.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly IExamService _examService;
        private readonly IUserRepository _userRepository;
        private IRequirementsForCategoryService _requiorementsForCategory;

        public AppointmentService(IRepository<Appointment> appointmentRepository, IUserRepository userRepository, IExamService examService, IRequirementsForCategoryService requiorementsForCategory)
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _examService = examService;
            _requiorementsForCategory = requiorementsForCategory;
        }

        public void ApproveAppointment(Guid? id)
        {
            Appointment a = GetAppointment(id);
            a.MeetsRequirements = MeetsRequirementsStatus.Approved;
            _appointmentRepository.Update(a);
        }

        public Appointment? CreateNewAppointment(string userId, Guid examId)
        {

            var user = _userRepository.Get(userId);

            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                CandidateId = user.Id,
                ExamId = examId,
                CreatedAt = DateTime.UtcNow,
                MeetsRequirements = MeetsRequirementsStatus.Pending,
                Status = AppointmentStatus.Pending,
                Exam = _examService.GetExam(examId),
                Candidate = _userRepository.Get(userId)
            };
            return _appointmentRepository.Insert(appointment);
        }

        public Appointment GetAppointment(Guid? id)
        {
            return _appointmentRepository.Get(id, z => z.Exam.TimeSlot, z => z.Exam.TimeSlot.Location, z => z.Candidate);
        }

        public List<Appointment> ListAllAppointments()
        {
            return _appointmentRepository.GetAll(z => z.Exam.TimeSlot, z => z.Exam.TimeSlot.Location, z => z.Candidate).ToList();
        }

        public void RejectAppointment(Guid? id, string rejectionReason)
        {
            Appointment a = GetAppointment(id);
            a.MeetsRequirements = MeetsRequirementsStatus.Rejected;
            a.RejectionReason = rejectionReason;
            _appointmentRepository.Update(a);
        }

        public bool UserMeetsRequirements(string userId, Guid examId)
        {
            var user = _userRepository.Get(userId);
            var exam = _examService.GetExam(examId);
            var requirements = _requiorementsForCategory.ListAllRequirementsForCategories().Where(z => z.CategoryId == exam.CategoryId).Select(z => z.Requirement);
            var requiredAge = requirements?.FirstOrDefault(r => r.RequiredAge != null)?.RequiredAge;
            if (requiredAge != null)
            {
                if (user.Age < requiredAge)
                {
                    return false;
                }

            }

            var candidateDocuments = user.Documents;
            if (candidateDocuments.Count != 0)
            {
                foreach (var requirement in requirements)
                {
                    if (!candidateDocuments.Any(d => d.DocumentTypeId == requirement?.DocumentTypeId))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
