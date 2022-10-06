using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Wrapper;
using usue_online_tests.Tests;

namespace usue_online_tests.Models
{
    public class TestWrapper
    {
        public ITest Test { get; set; }
        public int TestId { get; set; }
        public int Hash { get; set; }
        public string Link { get; set; }
        public string BtnText { get; set; }
        public bool TimeLimited { get; set; } = false;
        public int SecLimit { get; set; } = 60;
    }
}
