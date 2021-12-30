using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Tests
{
    public interface ITestCreater
    {
        static string Name { get; }
        ITest CreateTest(int randomSeed);
    }
}
