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
    public class TestPresetsEditController : Controller
    {
        private readonly DataContext _context;

        public TestPresetsEditController(DataContext context)
        {
            _context = context;
        }

        // GET: TestPresetsEdit
        public async Task<IActionResult> Index()
        {
            return View(await _context.Presets.ToListAsync());
        }

        // GET: TestPresetsEdit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testPreset = await _context.Presets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testPreset == null)
            {
                return NotFound();
            }

            return View(testPreset);
        }

        // GET: TestPresetsEdit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestPresetsEdit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tests,Name,MinutesToPass,TimeLimited")] TestPreset testPreset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testPreset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testPreset);
        }

        // GET: TestPresetsEdit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testPreset = await _context.Presets.FindAsync(id);
            if (testPreset == null)
            {
                return NotFound();
            }
            return View(testPreset);
        }

        // POST: TestPresetsEdit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tests,Name,MinutesToPass,TimeLimited")] TestPreset testPreset)
        {
            if (id != testPreset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testPreset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestPresetExists(testPreset.Id))
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
            return View(testPreset);
        }

        // GET: TestPresetsEdit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testPreset = await _context.Presets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testPreset == null)
            {
                return NotFound();
            }

            return View(testPreset);
        }

        // POST: TestPresetsEdit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testPreset = await _context.Presets.FindAsync(id);
            _context.Presets.Remove(testPreset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestPresetExists(int id)
        {
            return _context.Presets.Any(e => e.Id == id);
        }
    }
}
