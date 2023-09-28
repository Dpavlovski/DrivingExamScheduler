using DrivingExamScheduler.Domain.Models.Realation;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;

namespace DrivingExamScheduler.Service.Implementation
{
    public class RequirementForCategoryService : IRequirementsForCategoryService
    {
        private readonly IRepository<RequirementForCategory> _repository;

        public RequirementForCategoryService(IRepository<RequirementForCategory> repository)
        {
            _repository = repository;
        }

        public RequirementForCategory CreateNewRequirementForCategory(RequirementForCategory requirementForCategory)
        {
            throw new NotImplementedException();
        }

        public RequirementForCategory DeleteRequirementForCategory(Guid id)
        {
            throw new NotImplementedException();
        }

        public RequirementForCategory EditRequirementForCategory(RequirementForCategory requirementForCategory)
        {
            throw new NotImplementedException();
        }

        public RequirementForCategory GetRequirementForCategory(Guid? id)
        {
            throw new NotImplementedException();
        }

        public List<RequirementForCategory> ListAllRequirementsForCategories()
        {
            return _repository.GetAll(z => z.Requirement, z => z.Category).ToList();
        }
    }
}
