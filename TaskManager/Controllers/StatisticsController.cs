using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces.Services;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new StatisticsViewModel();
            viewModel.AverageUsedPercetageOfUsedTimeFromAllTasks = await _statisticsService.GetAverageUsedPercetageOfUsedTimeFromAllTasksAsync();
            viewModel.AverageUsedPercetageOfUsedTimeFromAllTasksGroupedByPriority = await _statisticsService.GetAverageUsedPercetageOfUsedTimeFromAllTasksGroupedByPriorityAsync();
            viewModel.AverageCompletedTasksPerMonth = await _statisticsService.GetAverageCompletedTasksPerMonthAsync();

            return View(viewModel);
        }
    }
}
