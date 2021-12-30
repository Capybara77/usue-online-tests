using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Roles Role { get; set; }
    }
}
