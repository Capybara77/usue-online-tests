using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
        List<MemoryStream> Pictures { get; set; }
    }

    public interface ITimeLimit
    {
        public int TimeLimitSeconds { get; set; }
    }

    public interface ITestGroup
    {
        public string GroupName { get; set; }
    }

    public interface IHidden
    {
        public bool IsHidden { get; set; }
    }
}
