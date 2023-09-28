using DrivingExamScheduler.Domain.Models.Domain;

namespace DrivingExamScheduler.Service.Interface
{
    public interface IExamService
    {
        public List<Exam> ListAllExams();

        public Exam GetExam(Guid? id);

        public Exam CreateNewExam(Exam Exam);

        public Exam EditExam(Exam exam);

        public Exam DeleteExam(Guid id);
    }
}
