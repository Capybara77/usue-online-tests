using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Tests
{
    public interface ITestCreator
    {
        int TestID { get; set; }
        string Name { get; }
        string Description { get; }
        ITest CreateTest(int randomSeed);
        int CheckAnswer(int randomSeed, Dictionary<string, string> answers);
    }
}
