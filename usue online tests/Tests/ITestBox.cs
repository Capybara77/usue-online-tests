using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Tests
{
    public interface ITestBox
    {
        List<string> Texts { get; set; }
        List<Bitmap> Pictures { get; set; }
        List<double> Numbers { get; set; }
        List<string> TextAnswers { get; set; }
    }
}
