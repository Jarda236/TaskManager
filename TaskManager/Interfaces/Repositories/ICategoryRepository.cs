using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllUserCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);

        Task<CategoryNumOfTasks> GetCategoryWithNumOfTasksAsync(int id);
    }
}
