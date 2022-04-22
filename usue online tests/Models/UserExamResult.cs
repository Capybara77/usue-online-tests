using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Models
{
    public class UserExamResult
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Exam Exam { get; set; }
        public DateTime DateTimeStart { get; set; }
        public ICollection<ExamTestAnswer> ExamTestAnswers { get; set; }
        public bool IsCompleted { get; set; }
    }
}
