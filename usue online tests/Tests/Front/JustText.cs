using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace usue_online_tests.Tests.Front;

public class JustText : ITestCreator, ITest
{
    public int TestID { get; set; }
    public string Name { get; } = "front";
    public string Description { get; } = "text";
    public ITest CreateTest(int randomSeed)
    {
        var test = new JustText
        {
            Text = "Просто текст в тесте\r\n" +
                   "123 321"
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