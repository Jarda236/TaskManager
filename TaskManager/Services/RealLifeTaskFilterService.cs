using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class RealLifeTaskFilterService : IRealLifeTaskFilterService
    {
        public IEnumerable<RealLifeTask> FilterTasks(IEnumerable<RealLifeTask> tasks, int? categoryId, Priority? priority, bool? isCompleted)
        {
            if (categoryId.HasValue)
            {
                tasks = tasks.Where(t => t.CategoryId == categoryId.Value);
            }
            if (priority.HasValue)
            {
                tasks = tasks.Where(t => t.Priority == priority.Value);
            }
            if (isCompleted.HasValue)
            {
                tasks = tasks.Where(t => t.IsCompleted == isCompleted.Value);
            }
            return tasks.ToList();
        }
    }
}
