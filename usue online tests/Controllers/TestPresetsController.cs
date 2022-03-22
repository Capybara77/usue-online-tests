using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using usue_online_tests.Data;
using usue_online_tests.Models;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class TestPresetsController : Controller
    {
        public GetUserByCookie UserByCookie { get; }
        public TestsLoader TestsLoader { get; }
        private readonly DataContext _context;

        public TestPresetsController(DataContext context, GetUserByCookie userByCookie, TestsLoader testsLoader)
        {
            UserByCookie = userByCookie;
            TestsLoader = testsLoader;
            _context = context;
        }

        // GET: TestPresets
        public async Task<IActionResult> Index()
        {
            ViewBag.Tests = TestsLoader.TestCreaters;
            return View(await _context.Presets.Where(preset => preset.Owner.Login == UserByCookie.GetUser().Login).ToListAsync());
        }

        // GET: TestPresets/Details/5
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

        // GET: TestPresets/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string tests)
        {
            TestPreset testPreset = new TestPreset
            {
                Tests = tests.Trim().Split(',').Select(s => Convert.ToInt32(s)).ToArray(),
                Name = name,
                Owner = UserByCookie.GetUser()
            };

            foreach (int testPresetTest in testPreset.Tests)
            {
                if (TestsLoader.TestCreaters.Count(creater => creater.TestID == testPresetTest) != 1)
                {
                    return StatusCode(400);
                }
            }

            _context.Presets.Add(testPreset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TestPresets/Edit/5
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

        // POST: TestPresets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tests,Name")] TestPreset testPreset)
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

        private bool TestPresetExists(int id)
        {
            return _context.Presets.Any(e => e.Id == id);
        }


        public async Task<RedirectToActionResult> Delete(int presetId)
        {
            var preset = _context.Presets.FirstOrDefault(preset => preset.Id == presetId && preset.Owner == UserByCookie.GetUser());

            if (preset != null)
            {
                var exams = _context.Exams.Where(exam => exam.Preset == preset).ToArray();
                foreach (Exam exam in exams)
                {
                    _context.Exams.Remove(exam);
                }

                _context.Presets.Remove(preset);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> StartNew(string group, DateTime dateTimeStart, DateTime dateTimeEnd, int presetId)
        {
            if (DateTime.Now > dateTimeStart || dateTimeStart > dateTimeEnd)
            {
                HttpContext.Response.StatusCode = 400;
                return Json("Некорректные входные данные");
            }

            _context.Exams.Add(new Exam
            {
                DateTimeStart = dateTimeStart,
                DateTimeEnd = dateTimeEnd,
                Group = group,
                Preset = _context.Presets.First(preset => preset.Id == presetId)
            });
            await _context.SaveChangesAsync();

            return View((object)new string ("Успешно добавлено"));
        }
    }
}
