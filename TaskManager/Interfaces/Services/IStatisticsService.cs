using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
    public interface IStatisticsService
    {
        Task<double> GetAverageUsedPercetageOfUsedTimeFromAllTasksAsync();
        Task<Dictionary<Priority, double>> GetAverageUsedPercetageOfUsedTimeFromAllTasksGroupedByPriorityAsync();
        Task<double> GetAverageCompletedTasksPerMonthAsync();
    }
}
