using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Test_Wrapper;
using usue_online_tests.Models;
using usue_online_tests.Tests;

namespace usue_online_tests.Report
{
    public class ExcelReportMaker : ReportMaker
    {
        public TestsLoader TestsLoader { get; }

        public ExcelReportMaker(IReportDataProvider dataProvider, TestsLoader testsLoader) : base(dataProvider)
        {
            TestsLoader = testsLoader;
        }

        public override IActionResult CreateReport(int examId)
        {
            DataProvider.SetExamId(examId);

            MemoryStream memoryStream = new MemoryStream();

            CreateExcelFile(memoryStream);

            memoryStream.Position = 0;
            return new FileStreamResult(memoryStream, "application/octet-stream")
            {
                FileDownloadName = $"report_{DataProvider.Exam.Preset.Name}_{DataProvider.Exam.Group}.xlsx"
            };
        }

        private void CreateExcelFile(MemoryStream memoryStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage(memoryStream);
            excelPackage.Workbook.Worksheets.Add("report");
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            int yPosition = 1;

            yPosition = CreateExcelStartBlock(worksheet, yPosition) + 2;
            yPosition = CreateExcelExamInfoBlock(worksheet, yPosition) + 2;
            yPosition = CreateExcelUsersResults(worksheet, yPosition) + 2;
            yPosition = CreateExcelTimeSpentToAnswer(worksheet, yPosition) + 2;
            yPosition = CreateExcelDoNotPass(worksheet, yPosition) + 2;
            yPosition = CreateExcelTestsInfo(worksheet, yPosition) + 2;

            AutoFitColumns(worksheet);

            excelPackage.SaveAs(memoryStream);
        }

        private int CreateExcelTimeSpentToAnswer(ExcelWorksheet worksheet, int y)
        {
            worksheet.Cells[y, 2].Value = $"Время, затраченное на прохождение тестов";
            worksheet.Cells[y + 1, 2].Value = $"Студент/задание";
            int testCount = DataProvider.Exam.Preset.Tests.Length;

            for (var i = 0; i < testCount; i++)
            {
                var testId = DataProvider.Exam.Preset.Tests[i];
                ITestCreator testCreator = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId);
                if (testCreator == null)
                {
                    worksheet.Cells[y + 1, i + 3].Value = testId;
                    worksheet.Cells[y + 1, i + 3].Style.TextRotation = 90;
                    continue;
                }

                worksheet.Cells[y + 1, i + 3].Value = testCreator.Name;
                worksheet.Cells[y + 1, i + 3].Style.TextRotation = 90;
            }

            worksheet.Cells[y + 1, 3 + testCount].Value = "Всего затрачено времени на прохождение";

            for (int i = 0; i < DataProvider.UsersExamResults.Length; i++)
            {
                UserExamResult result = DataProvider.UsersExamResults[i];
                ICollection<ExamTestAnswer> answers = result.ExamTestAnswers;

                worksheet.Cells[y + i + 2, 2].Value = DataProvider.UsersExamResults[i].User.Name;

                int totalSpentSec = 0;

                for (int j = 0; j < testCount; j++)
                {
                    int testId = DataProvider.Exam.Preset.Tests[j];
                    ExamTestAnswer userAnswer = answers.FirstOrDefault(
                        answer => answer.TestId == testId);
                    if (userAnswer == null) continue;

                    int spentSec = (int)(userAnswer.DateTimeEnd - userAnswer.DateTimeStart).TotalSeconds;
                    if (spentSec > 0)
                    {
                        totalSpentSec += spentSec;
                        worksheet.Cells[y + i + 2, 3 + j].Value = $"{spentSec}";
                    }
                    else
                    {
                        worksheet.Cells[y + i + 2, 3 + j].Value = $"?";
                    }
                }

                worksheet.Cells[y + i + 2, testCount + 3].Value = totalSpentSec;
            }

            return y + DataProvider.UsersExamResults.Length + 1;
        }

        private static void AutoFitColumns(ExcelWorksheet worksheet)
        {
            worksheet.Cells["A1:K20"].AutoFitColumns();
        }

        private int CreateExcelTestsInfo(ExcelWorksheet worksheet, int y)
        {
            if (DataProvider.UsersExamResults.Any())
            {
                var examTestsCreators = DataProvider.Exam.Preset.Tests
                    .Select(testId => TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId))
                    .Where(creator => creator != null)
                    .ToArray();

                worksheet.Cells[y, 2].Value = "Отчет по тестам";
                worksheet.Cells[y, 3].Value = "Правильные ответы";
                worksheet.Cells[y, 4].Value = "Всего ответов";
                worksheet.Cells[y, 5].Value = "Процент правильности";

                for (int i = 0; i < examTestsCreators.Length; i++)
                {
                    worksheet.Cells[y + 1 + i, 2].Value = examTestsCreators[i].Name;

                    int totalCorrect = DataProvider.UsersExamResults.Select(result =>
                            result.ExamTestAnswers.FirstOrDefault(answer => answer.TestId == examTestsCreators[i].TestID))
                        .Select(answer => answer?.CorrectAnswers ?? 0)
                        .Sum();

                    int total = DataProvider.UsersExamResults.Select(result =>
                            result.ExamTestAnswers.FirstOrDefault(answer => answer.TestId == examTestsCreators[i].TestID))
                        .Select(answer => answer?.TotalAnswers ?? 0)
                        .Sum();


                    worksheet.Cells[y + 1 + i, 3].Value = totalCorrect;
                    worksheet.Cells[y + 1 + i, 4].Value = total;
                    if (total != 0 && totalCorrect != -1)
                        worksheet.Cells[y + 1 + i, 5].Value = (int)((double)totalCorrect / total * 100);
                }

                return y + examTestsCreators.Length;
            }

            return y;
        }

        private int CreateExcelDoNotPass(ExcelWorksheet worksheet, int y)
        {
            worksheet.Cells[y, 2].Value = $"Студенты не прошедшие тест:";

            var students = DataProvider.GroupStudents.Where(user =>
                DataProvider.UsersExamResults.All(result => result.User.Id != user.Id)).ToArray();

            for (int i = 0; i < students.Length; i++)
            {
                worksheet.Cells[y + i + 1, 2].Value = students[i].Name;
            }

            return y + students.Length;
        }

        private int CreateExcelUsersResults(ExcelWorksheet worksheet, int y)
        {
            worksheet.Cells[y, 2].Value = $"Студент/задание";
            int testCount = DataProvider.Exam.Preset.Tests.Length;

            for (var i = 0; i < testCount; i++)
            {
                var testId = DataProvider.Exam.Preset.Tests[i];
                ITestCreator testCreator = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId);
                if (testCreator == null)
                {
                    worksheet.Cells[y, i + 3].Value = testId;
                    worksheet.Cells[y, i + 3].Style.TextRotation = 90;
                    continue;
                }

                worksheet.Cells[y, i + 3].Value = testCreator.Name;
                worksheet.Cells[y, i + 3].Style.TextRotation = 90;
            }

            worksheet.Cells[y, 3 + testCount].Value = "Итог";
            worksheet.Cells[y, 4 + testCount].Value = "Процент правильности";

            for (int i = 0; i < DataProvider.UsersExamResults.Length; i++)
            {
                UserExamResult result = DataProvider.UsersExamResults[i];
                ICollection<ExamTestAnswer> answers = result.ExamTestAnswers;

                int totalAnswers = 0;
                int totalCorrectAnswers = 0;

                worksheet.Cells[y + i + 1, 2].Value = DataProvider.UsersExamResults[i].User.Name;

                for (int j = 0; j < testCount; j++)
                {
                    int testId = DataProvider.Exam.Preset.Tests[j];
                    ExamTestAnswer userAnswer = answers.FirstOrDefault(
                        answer => answer.TestId == testId);
                    if (userAnswer == null) continue;

                    totalAnswers += userAnswer.TotalAnswers;
                    totalCorrectAnswers += userAnswer.CorrectAnswers;

                    worksheet.Cells[y + i + 1, 3 + j].Value = $"{userAnswer.CorrectAnswers}/{userAnswer.TotalAnswers}";

                    worksheet.Cells[y + 1 + i, 3 + j].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    double r = (double)userAnswer.CorrectAnswers / userAnswer.TotalAnswers;

                    if (r > .8)
                        worksheet.Cells[y + 1 + i, 3 + j].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                    else if (r > .4)
                        worksheet.Cells[y + 1 + i, 3 + j].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    else
                        worksheet.Cells[y + 1 + i, 3 + j].Style.Fill.BackgroundColor.SetColor(255, 255, 100, 100);
                }

                worksheet.Cells[y + i + 1, 3 + testCount].Value = $"{totalCorrectAnswers}/{totalAnswers}";
                worksheet.Cells[y + i + 1, 4 + testCount].Value = $"{(int)((double)totalCorrectAnswers / totalAnswers * 100)}";
            }

            return y + DataProvider.UsersExamResults.Length;
        }

        private int CreateExcelStartBlock(ExcelWorksheet worksheet, int y)
        {
            worksheet.Cells[y, 1].Value = "Отчет создан:";
            worksheet.Cells[y, 2].Value = DateTime.Now.ToNowEkb().ToString();

            return y;
        }

        private int CreateExcelExamInfoBlock(ExcelWorksheet worksheet, int y)
        {
            worksheet.Cells[y, 1].Value = $"Информация о тесте";
            worksheet.Cells[y, 2].Value = $"Группа";
            worksheet.Cells[y + 1, 2].Value = $"Дата выдачи";
            worksheet.Cells[y + 2, 2].Value = $"Дата окончания";
            worksheet.Cells[y + 3, 2].Value = $"Название шаблона";
            worksheet.Cells[y + 4, 2].Value = $"Ограничение по времени";

            worksheet.Cells[y, 3].Value = $"{DataProvider.Exam.Group}";
            worksheet.Cells[y + 1, 3].Value = $"{DataProvider.Exam.DateTimeStart}";
            worksheet.Cells[y + 2, 3].Value = $"{DataProvider.Exam.DateTimeEnd}";
            worksheet.Cells[y + 3, 3].Value = $"{DataProvider.Exam.Preset.Name}";
            worksheet.Cells[y + 4, 3].Value = $"{(DataProvider.Exam.Preset.TimeLimited ? "Да" : "Нет")}";

            return y + 4;
        }
    }
}