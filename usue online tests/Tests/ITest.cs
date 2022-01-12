using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace usue_online_tests.Tests
{
    public interface ITest
    {
        string Text { get; set; }
        string[] Inputs { get; set; }
        List<Bitmap> Pictures { get; set; }
    }
}
