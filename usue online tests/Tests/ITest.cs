using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace usue_online_tests.Tests
{
    public interface ITest
    {
        List<string> Texts { get; set; }
        List<double> Numbers { get; set; }
        List<Bitmap> Pictures { get; set; }


    }
}
