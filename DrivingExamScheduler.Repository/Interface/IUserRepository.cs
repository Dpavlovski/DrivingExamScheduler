using DrivingExamScheduler.Domain.Models.Identity;

namespace DrivingExamScheduler.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Candidate> GetAll();
        Candidate Get(string id);

    }
}