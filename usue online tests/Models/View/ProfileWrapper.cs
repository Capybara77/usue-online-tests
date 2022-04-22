using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Models.View
{
    public class ProfileWrapper
    {
        public User User { get; set; }
        public IEnumerable<UserExamResult> ExamResults { get; set; }
    }
}
