using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Services
{
    public class TestAnalyticsService : BackgroundService
    {
        public DataContext Data { get; }

        public TestAnalyticsService(DataContext data)
        {
            Data = data;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //return Task.Factory.StartNew(async () =>
            //{
            //    while (true)
            //    {
            //        await Task.Delay(5000, stoppingToken);
            //        if (stoppingToken.IsCancellationRequested) break;

            //        var b = Data.Exams.SelectMany(exam => Data.UserExamResults, (Exam exam, UserExamResult user) => new { exam, user })
            //            .Where(t => t.user.Exam == t.exam && t.user.IsCompleted != false && !t.exam.IsEnd && t.exam.DateTimeEnd < DateTime.Now)
            //            .Select(t => t.exam).ToList();

            //        foreach (Exam exam in b)
            //        {
            //            exam.IsEnd = true;
            //            Data.SaveChanges();
            //        }

            //    }
            //}, stoppingToken);
            return Task.CompletedTask;
        }
    }
}
