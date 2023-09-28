using DrivingExamScheduler.Domain.Models.Domain;

namespace DrivingExamScheduler.Service.Interface
{
    public interface IRequirementService
    {
        public List<Requirement> ListAllRequirements();

        public Requirement GetRequirement(Guid? id);

        public Requirement CreateNewRequirement(Requirement requirement);

        public Requirement EditRequirement(Requirement requirement);

        public Requirement DeleteRequirement(Guid id);
    }
}
