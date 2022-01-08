using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Tests
{
    public class SimpleTestBox : ITestBox
    {
        public List<string> Texts { get; set; } = new();
        public List<Bitmap> Pictures { get; set; } = new();
        public List<double> Numbers { get; set; } = new();
        public List<string> TextAnswers { get; set; } = new();
    }
}
