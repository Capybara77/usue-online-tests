using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Test_Wrapper;
#pragma warning disable CA1416

namespace usue_online_tests.Tests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]

    public class ExampleTest : ITestCreator
    {
        public int TestID { get; set; }
        public string Name => "Примет теста";
        public string Description => "Описание для теста";

        public ITest CreateTest(int randomSeed)
        {
            Random rand = new Random(randomSeed);

            ExampleTestResult result = new ExampleTestResult
            {
                Text = $"Текст задания. Ответом для этого задания является число - {rand.Next(0, 100)}\r\n" +
                       $"Введите ответ - \\(<user-answer:3>\\)"
            };
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random rand = new Random(randomSeed);
            int correctAnswer = rand.Next(0, 100);
            if (answers.ContainsKey("user-answer") && answers["user-answer"] == correctAnswer.ToString())
            {
                return 1;
            }
            return 0;
        }
    }

    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ExampleTestResult : ITest
    {
        public string Text { get; set; }
        public string[] CheckBoxes { get; set; } = {"Здесь можно поставить галочку"};
        public List<Image> Pictures { get; set; } = new()
            { Image.FromFile(Environment.CurrentDirectory + "\\wwwroot\\generators\\формулы.jpg") };
    }
}
