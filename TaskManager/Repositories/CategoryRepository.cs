using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public CategoryRepository(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            _context.Category.Add(category);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            _context.Category.Remove(new Category { Id = id });
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<Category>> GetAllUserCategoriesAsync()
        {
            return await _context.Category
                .Where(c => c.AppUserID == _userService.GetUserId())
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CategoryNumOfTasks> GetCategoryWithNumOfTasksAsync(int id)
        {
            return await _context.Category
                .Where(c => c.AppUserID == _userService.GetUserId())
                .Where(c => c.Id == id)
                .Select(c => new CategoryNumOfTasks
                {
                    Id = c.Id,
                    Name = c.Name,
                    NumOfTasks = c.RealLifeTasks.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            _context.Category.Update(category);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}
