using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class RealLifeTaskRepository : IRealLifeTaskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public RealLifeTaskRepository(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<bool> AddTaskAsync(RealLifeTask task)
        {
            _context.RealLifeTask.Add(task);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            _context.RealLifeTask.Remove(new RealLifeTask { Id = id });
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<RealLifeTask>> GetAllUserCompletedTasksAsync()
        {
            return await _context.RealLifeTask
                .Where(t => t.AppUserId == _userService.GetUserId() && t.IsCompleted)
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<RealLifeTask>> GetAllUserTasksAsync()
        {
            return await _context.RealLifeTask
                .Where(t => t.AppUserId == _userService.GetUserId())
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<RealLifeTask> GetTaskByIdAsync(int id)
        {
            return await _context.RealLifeTask
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateTaskAsync(RealLifeTask task)
        {
            _context.RealLifeTask.Update(task);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}
