using System;
using usue_online_tests.Models;

namespace usue_online_tests.Report
{
    public interface IReportDataProvider
    {
        Exam Exam { get; }
        UserExamResult[] UsersExamResults { get; }
        User[] GroupStudents { get; }

        public void SetExamId(int examId);
        Tuple<Exam, UserExamResult[]> GetReportData(int examId);

        PredictionResult[] PredictionResults { get; set; }

        void GetData();
    }
}