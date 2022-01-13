using System;
using System.Collections.Generic;
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

    //    public bool CheckAnswer(int randomSeed, string input, string value)
    //    {
    //        Random = new Random(randomSeed);
    //        int a = Random.Next(0, 20);
    //        string word = randomWords[Random.Next(0, randomWords.Length)];

    //        switch (input)
    //        {
    //            case "a":
    //                {
    //                    return a == Convert.ToInt32(value);
    //                }
    //            case "word":
    //                {
    //                    return word == value;
    //                }
    //        }

    //        return false;
    //    }

    //    public string Text { get; set; }
    //    public string[] Inputs { get; set; }
    //    public List<Bitmap> Pictures { get; set; }
    //}
}
