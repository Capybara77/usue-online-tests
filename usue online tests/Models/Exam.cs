using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public TestPreset Preset { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
    }
}
