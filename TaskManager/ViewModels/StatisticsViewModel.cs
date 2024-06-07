using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class StatisticsViewModel
    {
        public double AverageUsedPercetageOfUsedTimeFromAllTasks { get; set; }
        public Dictionary<Priority, double> AverageUsedPercetageOfUsedTimeFromAllTasksGroupedByPriority { get; set; }
        public double AverageCompletedTasksPerMonth { get; set; }
    }
}
