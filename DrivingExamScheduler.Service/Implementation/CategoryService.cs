using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Domain.Models.Realation;
using DrivingExamScheduler.Repository.Interface;
using DrivingExamScheduler.Service.Interface;

namespace DrivingExamScheduler.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Requirement> _requirementRepository;

        public CategoryService(IRepository<Category> categoryRepository, IRepository<Requirement> requirementRepository)
        {
            _categoryRepository = categoryRepository;
            _requirementRepository = requirementRepository;
        }

        public List<Category> ListAllCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public Category GetCategory(Guid? id)
        {
            return _categoryRepository.Get(id, z => z.RequirementsForCategory);
        }

        public Category CreateNewCategory(Category category, Guid[] selectedRequirements)
        {
            category.Id = Guid.NewGuid();
            ICollection<RequirementForCategory> requirements = new List<RequirementForCategory>();
            if (selectedRequirements != null)
            {
                foreach (var r in selectedRequirements)
                {
                    var requirement = new RequirementForCategory();
                    Requirement R = _requirementRepository.Get(r);
                    requirement.Requirement = R;
                    requirement.Category = category;
                    requirement.RequirementId = r;
                    requirement.CategoryId = category.Id;
                    requirements.Add(requirement);
                }
            }

            category.RequirementsForCategory = requirements;
            return _categoryRepository.Insert(category);
        }

        public Category EditCategory(Category category, Guid[] selectedRequirements)
        {
            ICollection<RequirementForCategory> requirements = new List<RequirementForCategory>();
            if (selectedRequirements != null)
            {
                foreach (var r in selectedRequirements)
                {
                    var requirement = new RequirementForCategory();
                    Requirement R = _requirementRepository.Get(r);
                    requirement.Requirement = R;
                    requirement.Category = category;
                    requirement.RequirementId = r;
                    requirement.CategoryId = category.Id;
                    requirements.Add(requirement);
                }
            }

            category.RequirementsForCategory = requirements;
            return _categoryRepository.Update(category);
        }

        public Category DeleteCategory(Guid id)
        {
            var category = GetCategory(id);
            return _categoryRepository.Delete(category);
        }

    }
}
