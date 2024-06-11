using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
    public interface IRealLifeTaskFilterService
    {
        public IEnumerable<RealLifeTask> FilterTasks(IEnumerable<RealLifeTask> tasks, int? categoryId, Priority? priority, bool? isCompleted);
    }
}
