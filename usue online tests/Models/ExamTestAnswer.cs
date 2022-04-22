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
    }
}