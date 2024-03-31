﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
using usue_online_tests;

namespace usue_online_tests.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class TestPresetsController : Controller
    {
        public GetUserByCookie UserByCookie { get; }
        public TestsLoader TestsLoader { get; }
        private readonly DataContext context;

        public TestPresetsController(DataContext context, GetUserByCookie userByCookie, TestsLoader testsLoader)
        {
            UserByCookie = userByCookie;
            TestsLoader = testsLoader;
            this.context = context;
        }

        // GET: TestPresets
        public async Task<IActionResult> Index()
        {
            ViewBag.Tests = TestsLoader.TestCreators;
            return View(await context.Presets.Where(preset => preset.Owner.Id == UserByCookie.GetUser().Id)
                .ToListAsync());
        }

        // GET: TestPresets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testPreset = await context.Presets
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
            if (name.Length > 80)
                return StatusCode(400);

            bool timeLimited = Request.Form["timeLimited"].FirstOrDefault() == "on";
            bool homework = Request.Form["homework"].FirstOrDefault() == "on";

            timeLimited = !homework && timeLimited;

            TestPreset testPreset = new TestPreset
            {
                Tests = tests.Trim().Split(',').Select(s => Convert.ToInt32(s)).ToArray(),
                Name = name,
                Owner = UserByCookie.GetUser(),
                TimeLimited = timeLimited,
                IsHomework = homework
            };

            if (testPreset.Tests.Length == 0) return StatusCode(400);

            foreach (int testPresetTest in testPreset.Tests)
            {
                if (TestsLoader.TestCreators.Count(creator => creator.TestID == testPresetTest) != 1)
                {
                    return StatusCode(400);
                }
            }

            context.Presets.Add(testPreset);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TestPresets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testPreset = await context.Presets.FindAsync(id);
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
                    context.Update(testPreset);
                    await context.SaveChangesAsync();
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
            return context.Presets.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route($"/testpresets/Delete/{{presetId}}")]
        public IActionResult DeleteConfirm(int presetId)
        {
            User user = UserByCookie.GetUser();

            if (!context.Presets.Any(preset => preset.Id == presetId && preset.Owner.Id == user.Id))
            {
                return Redirect("/testpresets");
            }

            TestPreset preset = context.Presets.First(testPreset => testPreset.Id == presetId);

            return View("ConfirmDelete", preset);
        }

        [HttpPost]
        public RedirectToActionResult Delete(int presetId)
        {
            var preset = context.Presets.FirstOrDefault(preset => preset.Id == presetId && preset.Owner == UserByCookie.GetUser());

            if (preset != null)
            {
                var data = context.UserExamResults
                    .Where(result => result.Exam.Preset.Id == presetId)
                    .Include(result => result.ExamTestAnswers).ToArray();

                var exams = context.Exams.Where(exam => exam.Preset.Id == presetId);

                //var exams = context.Exams.Where(exam => exam.Preset == preset).ToArray();
                //foreach (Exam exam in exams)
                //{
                //    context.Exams.Remove(exam);
                //}

                context.UserExamResults.RemoveRange(data);
                context.Exams.RemoveRange(exams);
                context.Presets.Remove(preset);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> StartNew(string group, DateTime dateTimeStart, DateTime dateTimeEnd, int presetId)
        {
            if (dateTimeStart > dateTimeEnd)
            {
                HttpContext.Response.StatusCode = 400;
                return Json("Некорректные входные данные");
            }

            if (!context.Users.Any(user => user.Group == group)) return StatusCode(400);

            context.Exams.Add(new Exam
            {
                DateTimeStart = dateTimeStart,
                DateTimeEnd = dateTimeEnd,
                Group = group,
                Preset = context.Presets.First(preset => preset.Id == presetId)
            });
            await context.SaveChangesAsync();

            return View((object)new string("Успешно добавлено"));
        }
    }
}