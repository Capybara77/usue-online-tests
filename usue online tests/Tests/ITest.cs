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
        List<Image> Pictures { get; set; }
    }
}
