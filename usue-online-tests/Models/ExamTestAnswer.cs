using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace usue_online_tests.Models
{
    public class ExamTestAnswer
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public int CorrectAnswers { get; set; }

        public int TotalAnswers { get; set; }

        public DateTime DateTimeStart { get; set; }

        public DateTime DateTimeEnd { get; set; }

        public int UserExamResultId { get; set; }

        [ForeignKey(nameof(UserExamResultId))]
        public UserExamResult UserExamResult { get; set; }
    }
}