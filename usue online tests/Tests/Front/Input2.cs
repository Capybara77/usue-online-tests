using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace usue_online_tests.Tests.Front;

public class Input2 : ITestCreator, ITest
{
    public int TestID { get; set; }
    public string Name { get; } = "front";
    public string Description { get; } = "front FormInput";
    public ITest CreateTest(int randomSeed)
    {
        var test = new Input2
        {
            Text = "\\(\\FormInput[1][input input-sm mx-2 my-1][]{name}\\)"
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