using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserExamResultsController : Controller
    {
        private readonly DataContext _context;

        public UserExamResultsController(DataContext context)
        {
            _context = context;
        }

        // GET: UserExamResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserExamResults
                .Include(result => result.Exam)
                .Include(result => result.Exam.Preset)
                .Include(result => result.ExamTestAnswers)
                .ToListAsync());
        }

        // GET: UserExamResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExamResult = await _context.UserExamResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExamResult == null)
            {
                return NotFound();
            }

            return View(userExamResult);
        }

        // GET: UserExamResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserExamResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTimeStart,IsCompleted")] UserExamResult userExamResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userExamResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userExamResult);
        }

        // GET: UserExamResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExamResult = await _context.UserExamResults.FindAsync(id);
            if (userExamResult == null)
            {
                return NotFound();
            }
            return View(userExamResult);
        }

        // POST: UserExamResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTimeStart,IsCompleted")] UserExamResult userExamResult)
        {
            if (id != userExamResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userExamResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExamResultExists(userExamResult.Id))
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
            return View(userExamResult);
        }

        // GET: UserExamResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExamResult = await _context.UserExamResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExamResult == null)
            {
                return NotFound();
            }

            return View(userExamResult);
        }

        // POST: UserExamResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userExamResult = await _context.UserExamResults.FindAsync(id);
            _context.UserExamResults.Remove(userExamResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExamResultExists(int id)
        {
            return _context.UserExamResults.Any(e => e.Id == id);
        }
    }
}
