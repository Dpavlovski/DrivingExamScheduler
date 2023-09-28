using DrivingExamScheduler.Domain.Models.Domain;

namespace DrivingExamScheduler.Service.Interface
{
    public interface ICategoryService
    {
        public List<Category> ListAllCategories();

        public Category GetCategory(Guid? id);

        public Category CreateNewCategory(Category category, Guid[] selectedRequirements);

        public Category EditCategory(Category category, Guid[] selectedRequirements);

        public Category DeleteCategory(Guid id);

    }
}
