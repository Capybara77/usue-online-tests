using System.Security.Permissions;

namespace usue_online_tests.Models
{
    public class ExamWrapper
    {
        public TestWrapper TestWrapper { get; set; }
        public TestPreset TestPreset { get; set; }
        public bool ChangeAnswer { get; set; }
        public ExamTestAnswer OldTestResult { get; set; }
        public int ExamId { get; set; }
        public bool SaveResult { get; set; }
    }
}
