using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Models
{
    public class TestResult
    {
        public double[] UserNumbers { get; set; }

        public string[] UserTextAnswers { get; set; }

        public int TotalRight { get; set; }

        public int Total { get; set; }

        public Exception? Exception { get; set; }

        //public double[] RightNumbers { get; set; }
        //public double[] RightTextAnswers { get; set; }
    }
}
