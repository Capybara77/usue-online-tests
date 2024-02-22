using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Test_Wrapper;

namespace usue_online_tests.Tests.Front;

public class SimpleFormula : ITestCreator, ITest
{
    public int TestID { get; set; }
    public string Name { get; } = "front";
    public string Description { get; } = "formula";
    public ITest CreateTest(int randomSeed)
    {
        Thread.Sleep(2000);
        var test = new SimpleFormula
        {
            Text = "\\(x^2=43_2\\)"
        };

        return test;
    }

    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    {
        throw new System.NotImplementedException();
    }

    public string Text { get; set; }
    public string[] CheckBoxes { get; set; }
    public List<Image> Pictures { get; set; }
}