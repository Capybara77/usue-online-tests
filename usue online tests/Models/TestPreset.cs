using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Models
{
    public class TestPreset
    {
        public int Id { get; set; }
        public int[] Tests { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public bool TimeLimited { get; set; } = false;
    }
}
