using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace usue_online_tests.Tests
{
    //public class SimpleTest : ITestCreater, ITest
    //{
    //    private string[] randomWords = new[] { "саня", "набеков", "стас" };

    //    public int TestID { get; set; }
    //    public string Name { get; } = "Simple Test";
    //    public string Description { get; } = "Easy test.";

    //    public Random Random { get; set; }

    //    public ITest CreateTest(int randomSeed)
    //    {
    //        ITest result = new SimpleTest();
    //        Random = new Random(randomSeed);
    //        int a = Random.Next(0, 20);
    //        result.Text = $"Решите уравнение: \\(<a>\\) + 5 = {a + 5}\r\n";

    //        string word = randomWords[Random.Next(0, randomWords.Length)];

    //        result.Text += $"Напишите слово полностью **{word.Remove(0, 2)}\r\n\\(<word>\\)";

    //        return result;
    //    }

    //    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    //    {
    //        Random = new Random(randomSeed);
    //        int a = Random.Next(0, 20);
    //        string word = randomWords[Random.Next(0, randomWords.Length)];
    //        int total = 0;

    //        foreach (var key in answers.Keys)
    //        {
    //            switch (key)
    //            {
    //                case "a":
    //                {
    //                    if (a == Convert.ToInt32(answers[key])) total++;
    //                    break;
    //                }
    //                case "word":
    //                {
    //                    if (word == answers[key]) total++;
    //                    break;
    //                }
    //            }

    //        }

    //        return total;
    //    }


    //    public string Text { get; set; }
    //    public List<Image> Pictures { get; set; }
    //}
}
