using DrivingExamScheduler.Domain.Models.Realation;

namespace DrivingExamScheduler.Service.Interface
{
    public interface IRequirementsForCategoryService
    {
        public List<RequirementForCategory> ListAllRequirementsForCategories();

        public RequirementForCategory GetRequirementForCategory(Guid? id);

        public RequirementForCategory CreateNewRequirementForCategory(RequirementForCategory requirementForCategory);

        public RequirementForCategory EditRequirementForCategory(RequirementForCategory requirementForCategory);

        public RequirementForCategory DeleteRequirementForCategory(Guid id);
    }
}
