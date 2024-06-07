using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IRealLifeTaskRepository
    {
        Task<IEnumerable<RealLifeTask>> GetAllUserTasksAsync();
        Task<RealLifeTask> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(RealLifeTask task);
        Task<bool> UpdateTaskAsync(RealLifeTask task);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<RealLifeTask>> GetAllUserCompletedTasksAsync();

    }
}
