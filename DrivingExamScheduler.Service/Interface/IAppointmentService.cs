using DrivingExamScheduler.Domain.Models.Domain;

namespace DrivingExamScheduler.Service.Interface
{
    public interface IAppointmentService
    {
        public List<Appointment> ListAllAppointments();

        public Appointment GetAppointment(Guid? id);

        public Appointment? CreateNewAppointment(string userId, Guid examId);

        public bool UserMeetsRequirements(string userId, Guid examId);

        public void ApproveAppointment(Guid? id);

        public void RejectAppointment(Guid? id, string rejectionReason);
    }
}
