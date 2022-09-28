using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
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
            yPosition = CreateExcelDoNotPass(worksheet, yPosition) + 2;
            yPosition = CreateExcelTestsInfo(worksheet, yPosition) + 2;

            AutoFitColumns(worksheet);

            excelPackage.SaveAs(memoryStream);
        }

        private static void AutoFitColumns(ExcelWorksheet worksheet)
        {
            worksheet.Cells["A1:K20"].AutoFitColumns();
        }

        private int CreateExcelTestsInfo(ExcelWorksheet worksheet, int y)
        {
            if (DataProvider.UsersExamResults.Any())
            {
                //var examTestsCreators = DataProvider.UsersExamResults.First().ExamTestAnswers.Select(answer =>
                //    TestsLoader.TestCreators.First(creator => creator.TestID == answer.TestId)).ToArray();

                var examTestsCreators = DataProvider.Exam.Preset.Tests
                    .Select(testId => TestsLoader.TestCreators.First(creator => creator.TestID == testId)).ToArray();

                worksheet.Cells[y, 2].Value = "Отчет по тестам";
                worksheet.Cells[y, 3].Value = "Правильные ответы";
                worksheet.Cells[y, 4].Value = "Всего ответов";
                worksheet.Cells[y, 5].Value = "Процент правильности";

                for (int i = 0; i < examTestsCreators.Length; i++)
                {
                    worksheet.Cells[y + 1 + i, 2].Value = examTestsCreators[i].Name;

                    int totalCorrect = DataProvider.UsersExamResults.Select(result =>
                            result.ExamTestAnswers.FirstOrDefault(answer => answer.TestId == examTestsCreators[i].TestID))
                        .Select(answer => answer!.CorrectAnswers)
                        .Sum();

                    int total = DataProvider.UsersExamResults.Select(result =>
                            result.ExamTestAnswers.FirstOrDefault(answer => answer.TestId == examTestsCreators[i].TestID))
                        .Select(answer => answer!.TotalAnswers)
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
            worksheet.Cells[y, 2].Value = $"Студент/задания";

            for (int i = 0; i < DataProvider.UsersExamResults.Length; i++)
            {
                worksheet.Cells[y + i + 1, 2].Value = $"{DataProvider.UsersExamResults[i].User.Name}";

                var answersList = new List<ExamTestAnswer>(DataProvider.UsersExamResults[i].ExamTestAnswers);

                int totalCorrect = 0;
                int total = 0;

                for (int j = 0; j < answersList.Count; j++)
                {
                    totalCorrect += answersList[j].CorrectAnswers;
                    total += answersList[j].TotalAnswers;

                    worksheet.Cells[y + i + 1, 3 + j].Value = $"{answersList[j].CorrectAnswers}/{answersList[j].TotalAnswers}";

                    worksheet.Cells[y + 1 + i, 3 + j].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    double r = (double)answersList[j].CorrectAnswers / answersList[j].TotalAnswers;

                    if (r > .8)
                        worksheet.Cells[y + 1 + i, 3 + j].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                    else if (r > .5)
                        worksheet.Cells[y + 1 + i, 3 + j].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    else
                        worksheet.Cells[y + 1 + i, 3 + j].Style.Fill.BackgroundColor.SetColor(Color.Red);
                }

                worksheet.Cells[y + i + 1, 3 + answersList.Count].Value = $"{totalCorrect}/{total}";
                worksheet.Cells[y + i + 1, 4 + answersList.Count].Value = $"{(int)((double)totalCorrect / total * 100)}";
            }

            if (DataProvider.UsersExamResults.Any())
            {
                var answersList = new List<ExamTestAnswer>(DataProvider.UsersExamResults.First().ExamTestAnswers);

                for (int i = 0; i < answersList.Count; i++)
                {
                    worksheet.Cells[y, 3 + i].Value = TestsLoader.TestCreators
                        .First(creator => creator.TestID == answersList[i].TestId).Name;
                }

                worksheet.Cells[y, 3 + answersList.Count].Value = "Итог";
                worksheet.Cells[y, 4 + answersList.Count].Value = "Процент правильности";
            }

            return y + DataProvider.UsersExamResults.Length;
        }

        private int CreateExcelStartBlock(ExcelWorksheet worksheet, int y)
        {
            worksheet.Cells[y, 1].Value = "Отчет создан:";
            worksheet.Cells[y, 2].Value = DateTime.Now.ToString();

            return y;
        }

        private int CreateExcelExamInfoBlock(ExcelWorksheet worksheet, int y)
        {
            worksheet.Cells[y, 1].Value = $"Информация о тесте";
            worksheet.Cells[y, 2].Value = $"Группа";
            worksheet.Cells[y + 1, 2].Value = $"Дата выдачи";
            worksheet.Cells[y + 2, 2].Value = $"Дата окончания";
            worksheet.Cells[y + 3, 2].Value = $"Название шаблона";

            worksheet.Cells[y, 3].Value = $"{DataProvider.Exam.Group}";
            worksheet.Cells[y + 1, 3].Value = $"{DataProvider.Exam.DateTimeStart}";
            worksheet.Cells[y + 2, 3].Value = $"{DataProvider.Exam.DateTimeEnd}";
            worksheet.Cells[y + 3, 3].Value = $"{DataProvider.Exam.Preset.Name}";

            return y + 3;
        }
    }
}
