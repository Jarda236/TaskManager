using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRealLifeTaskRepository _realLifeTaskRepository;

        public StatisticsService(IRealLifeTaskRepository realLifeTaskRepository)
        {
            _realLifeTaskRepository = realLifeTaskRepository;
        }

        /// <summary>
        /// Returns the average number of completed tasks per months of all time
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetAverageCompletedTasksPerMonthAsync()
        {
            var completedTasks = await _realLifeTaskRepository.GetAllUserCompletedTasksAsync();
            var months = completedTasks.Select(t => t.CompletedAt.Value.Month).Distinct().Count();
            return completedTasks.Count() / months;

        }

        /// <summary>
        /// Returns the average percentage of used time of all tasks.
        /// Used time is the time between the creation of the task and the completion of the task.
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetAverageUsedPercetageOfUsedTimeFromAllTasksAsync()
        {
            var completedTasks = await _realLifeTaskRepository.GetAllUserCompletedTasksAsync();

            var usedTime = completedTasks.Select(t => (t.CompletedAt.Value - t.CreatedAt).TotalDays).Sum();
            var totalTime = completedTasks.Select(t => (t.Deadline - t.CreatedAt).TotalDays).Sum();
            return usedTime / totalTime;

        }

        public async Task<Dictionary<Priority, double>> GetAverageUsedPercetageOfUsedTimeFromAllTasksGroupedByPriorityAsync()
        {
            var CompletedTasks = await _realLifeTaskRepository.GetAllUserCompletedTasksAsync();

            var groupedTasks = CompletedTasks.GroupBy(t => t.Priority);

            var result = new Dictionary<Priority, double>();
            foreach (var group in groupedTasks)
            {
                var usedTime = group.Select(t => (t.CompletedAt.Value - t.CreatedAt).TotalDays).Sum();
                var totalTime = group.Select(t => (t.Deadline - t.CreatedAt).TotalDays).Sum();
                result.Add(group.Key, usedTime / totalTime);
            }
            return result;
        }
    }
}
