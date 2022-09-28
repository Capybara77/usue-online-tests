using System;
using System.Collections.Generic;

namespace usue_online_tests.Models
{
    public class TeacherProfileWrapper : IProfileWrapper
    {
        public User User { get; set; }
        public ExamResult[] ExamResults { get; set; }
    }

    public class ExamResult
    {
        public Exam Exam { get; set; }
        public UserExamResult[] Results { get; set; }
    }

    public class EqualityComparerExamResult : IEqualityComparer<ExamResult>
    {
        public bool Equals(ExamResult x, ExamResult y)
        {
            return y != null && x != null && x.Exam.Id == y.Exam.Id;
        }

        public int GetHashCode(ExamResult obj)
        {
            return obj.Exam.Id.GetHashCode();
        }
    }
}
