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
    public class ExamTestAnswersController : Controller
    {
        private readonly DataContext _context;

        public ExamTestAnswersController(DataContext context)
        {
            _context = context;
        }

        // GET: ExamTestAnswers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExamTestAnswers
                .ToListAsync());
        }

        // GET: ExamTestAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTestAnswer = await _context.ExamTestAnswers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examTestAnswer == null)
            {
                return NotFound();
            }

            return View(examTestAnswer);
        }

        // GET: ExamTestAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExamTestAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TestId,CorrectAnswers,TotalAnswers")] ExamTestAnswer examTestAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examTestAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(examTestAnswer);
        }

        // GET: ExamTestAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTestAnswer = await _context.ExamTestAnswers.FindAsync(id);
            if (examTestAnswer == null)
            {
                return NotFound();
            }
            return View(examTestAnswer);
        }

        // POST: ExamTestAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TestId,CorrectAnswers,TotalAnswers")] ExamTestAnswer examTestAnswer)
        {
            if (id != examTestAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examTestAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamTestAnswerExists(examTestAnswer.Id))
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
            return View(examTestAnswer);
        }

        // GET: ExamTestAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTestAnswer = await _context.ExamTestAnswers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examTestAnswer == null)
            {
                return NotFound();
            }

            return View(examTestAnswer);
        }

        // POST: ExamTestAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examTestAnswer = await _context.ExamTestAnswers.FindAsync(id);
            _context.ExamTestAnswers.Remove(examTestAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamTestAnswerExists(int id)
        {
            return _context.ExamTestAnswers.Any(e => e.Id == id);
        }
    }
}
