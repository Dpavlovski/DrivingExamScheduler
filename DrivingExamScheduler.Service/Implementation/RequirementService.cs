using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;

namespace DrivingExamScheduler.Service.Implementation
{
    public class RequirementService : IRequirementService
    {
        private readonly IRepository<Requirement> _requirementRepository;

        public RequirementService(IRepository<Requirement> requirementRepository)
        {
            _requirementRepository = requirementRepository;
        }
        public Requirement CreateNewRequirement(Requirement requirement)
        {
            requirement.Id = Guid.NewGuid();
            return _requirementRepository.Insert(requirement);
        }

        public Requirement DeleteRequirement(Guid id)
        {
            var requirement = GetRequirement(id);
            return _requirementRepository.Delete(requirement);
        }

        public Requirement EditRequirement(Requirement requirement)
        {
            return _requirementRepository.Update(requirement);
        }

        public Requirement GetRequirement(Guid? id)
        {
            return _requirementRepository.Get(id, z => z.RequiredCategory, z => z.DocumentType);
        }

        public List<Requirement> ListAllRequirements()
        {
            return _requirementRepository.GetAll(z => z.RequiredCategory, z => z.DocumentType).ToList();
        }
    }
}
