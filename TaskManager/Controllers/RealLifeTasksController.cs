using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize]
    public class RealLifeTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RealLifeTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RealLifeTasks
        public async Task<IActionResult> Index()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userID == null)
            {
                return NotFound();
            }
            

            var tasks = await _context.RealLifeTask
                .Where(t => t.UserId == userID)
                .ToListAsync();

            return View(tasks);
        }

        // GET: RealLifeTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realLifeTask = await _context.RealLifeTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realLifeTask == null)
            {
                return NotFound();
            }

            return View(realLifeTask);
        }

        // GET: RealLifeTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RealLifeTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Category,IsCompleted,CreatedAt,Deadline,CompletedAt")] RealLifeTask realLifeTask)
        {
            realLifeTask.CreatedAt = DateTime.Now;
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userID == null)
            {
                return NotFound();
            }

            realLifeTask.UserId = userID;

            if (ModelState.IsValid)
            {
                _context.Add(realLifeTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(realLifeTask);
        }

        // GET: RealLifeTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realLifeTask = await _context.RealLifeTask.FindAsync(id);
            if (realLifeTask == null)
            {
                return NotFound();
            }
            return View(realLifeTask);
        }

        // POST: RealLifeTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Category,IsCompleted,CreatedAt,Deadline,CompletedAt")] RealLifeTask realLifeTask)
        {
            if (id != realLifeTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realLifeTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealLifeTaskExists(realLifeTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(realLifeTask);
        }

        // GET: RealLifeTasks/Complete/5
        public async Task<IActionResult> Complete(int id)
        {
            var realLifeTask = await _context.RealLifeTask.FindAsync(id);
            if (realLifeTask == null)
            {
                return NotFound();
            }

            realLifeTask.IsCompleted = true;
            realLifeTask.CompletedAt = DateTime.Now;

            _context.Update(realLifeTask);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: RealLifeTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realLifeTask = await _context.RealLifeTask
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var realLifeTask = await _context.RealLifeTask.FindAsync(id);
            if (realLifeTask != null)
            {
                _context.RealLifeTask.Remove(realLifeTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool RealLifeTaskExists(int id)
        {
            return _context.RealLifeTask.Any(e => e.Id == id);
        }
    }
}
