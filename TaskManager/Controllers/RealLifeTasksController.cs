using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Extensions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    [Authorize]
    public class RealLifeTasksController : Controller
    {
        private readonly IRealLifeTaskRepository _realLifeTaskRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRealLifeTaskFilterService _realLifeTaskFilterService;

        public RealLifeTasksController(IRealLifeTaskRepository realLifeTaskRepository, ICategoryRepository categoryRepository, IRealLifeTaskFilterService realLifeTaskFilterService)
        {
            _realLifeTaskRepository = realLifeTaskRepository;
            _categoryRepository = categoryRepository;
            _realLifeTaskFilterService = realLifeTaskFilterService;
        }

        // GET: RealLifeTasks
        public async Task<IActionResult> Index(int? categoryId, Priority? priority, bool? isCompleted)
        {
            var userID = User.GetId();

            if (userID == null)
            {
                return NotFound();
            }

            var categories = await _categoryRepository.GetAllUserCategoriesAsync();
            ViewBag.Categories = categories;

            var tasks = await _realLifeTaskRepository.GetAllUserTasksAsync();
            tasks = _realLifeTaskFilterService.FilterTasks(tasks, categoryId, priority, isCompleted);

            var viewModel = new RealLifeTaskFilterViewModel
            {
                Tasks = tasks,
                CategoryId = categoryId,
                Priority = priority,
                IsCompleted = isCompleted,
            };

            return View(viewModel);
        }

        // GET: RealLifeTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realLifeTask = await _realLifeTaskRepository.GetTaskByIdAsync(id.Value);

            if (realLifeTask == null)
            {
                return NotFound();
            }

            return View(realLifeTask);
        }

        // GET: RealLifeTasks/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllUserCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "Name");

            return View();
        }

        // POST: RealLifeTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RealLifeTaskCreateViewModel viewModel)
        {
            var userID = User.GetId();

            if (userID == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var realLifeTask = new RealLifeTask
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    CategoryId = viewModel.CategoryId,
                    Priority = viewModel.Priority,
                    Deadline = viewModel.Deadline,
                    CreatedAt = DateTime.Now,
                    AppUserId = userID
                };

                await _realLifeTaskRepository.AddTaskAsync(realLifeTask);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetAllUserCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(viewModel);
        }

        // GET: RealLifeTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realLifeTask = await _realLifeTaskRepository.GetTaskByIdAsync(id.Value);
            if (realLifeTask == null)
            {
                return NotFound();
            }

            var categories = await _categoryRepository.GetAllUserCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var viewModel = new RealLifeTaskEditViewModel
            {
                Id = realLifeTask.Id,
                Name = realLifeTask.Name,
                Description = realLifeTask.Description,
                CategoryId = realLifeTask.CategoryId,
                Priority = realLifeTask.Priority,
                Deadline = realLifeTask.Deadline,
                IsCompleted = realLifeTask.IsCompleted,
                CreatedAt = realLifeTask.CreatedAt
            };

            return View(viewModel);
        }

        // POST: RealLifeTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RealLifeTaskEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            var userID = User.GetId();

            if (userID == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var realLifeTask = await _realLifeTaskRepository.GetTaskByIdAsync(id);
                if (realLifeTask == null)
                {
                    return NotFound();
                }

                realLifeTask.Name = viewModel.Name;
                realLifeTask.Description = viewModel.Description;
                realLifeTask.CategoryId = viewModel.CategoryId;
                realLifeTask.Priority = viewModel.Priority;
                realLifeTask.Deadline = viewModel.Deadline;
                realLifeTask.IsCompleted = viewModel.IsCompleted;
                realLifeTask.CreatedAt = viewModel.CreatedAt;
                realLifeTask.AppUserId = userID;

                await _realLifeTaskRepository.UpdateTaskAsync(realLifeTask);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllUserCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(viewModel);
        }

        // GET: RealLifeTasks/Complete/5
        public async Task<IActionResult> Complete(int id)
        {
            var realLifeTask = await _realLifeTaskRepository.GetTaskByIdAsync(id);
            if (realLifeTask == null)
            {
                return NotFound();
            }

            realLifeTask.IsCompleted = true;
            realLifeTask.CompletedAt = DateTime.Now;

            await _realLifeTaskRepository.UpdateTaskAsync(realLifeTask);

            return RedirectToAction(nameof(Index));
        }

        // GET: RealLifeTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realLifeTask = await _realLifeTaskRepository.GetTaskByIdAsync(id.Value);
            if (realLifeTask == null)
            {
                return NotFound();
            }

            return View(realLifeTask);
        }

        // POST: RealLifeTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _realLifeTaskRepository.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
