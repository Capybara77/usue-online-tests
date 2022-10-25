using System;
using System.Collections.Generic;
using System.Drawing;

namespace Test_Wrapper
{
    public interface ITestCreator
    {
        int TestID { get; set; }
        string Name { get; }
        string Description { get; }
        ITest CreateTest(int randomSeed);
        int CheckAnswer(int randomSeed, Dictionary<string, string> answers);
    }
    public interface ITest
    {
        string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        List<Image> Pictures { get; set; }
    }

    public interface ITimeLimit
    {
        int TimeLimitSeconds { get; set; }
    }

    public interface ITestGroup
    {
        public string GroupName { get; set; }
    }
}
