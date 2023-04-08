using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Report
{
    public class DbDataProvider : IReportDataProvider
    {
        private Exam _exam;
        private UserExamResult[] _userExamResults;
        private User[] _groupStudents;
        private DataContext DataContext { get; }
        private int ExamId { get; set; }

        public Exam Exam
        {
            get => _exam ??= DataContext.Exams.Include(exam => exam.Preset).First(exam => exam.Id == ExamId);
            private set => _exam = value;
        }

        public UserExamResult[] UsersExamResults
        {
            get => _userExamResults ??= DataContext.Exams
                    .Where(exam => exam.Id == ExamId)
                    .SelectMany(exam => DataContext.UserExamResults.Where(result => result.Exam.Id == ExamId))
                    .Include(result => result.ExamTestAnswers)
                    .Include(result => result.User)
                    .OrderBy(result => result.User.Name)
                    .ToArray();
            private set => _userExamResults = value;
        }

        public User[] GroupStudents
        {
            get => _groupStudents ??= DataContext.Users.Where(user => user.Group == Exam.Group).ToArray();
            private set => _groupStudents = value;
        }

        public DbDataProvider(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public void SetExamId(int examId) => ExamId = examId;

        public Tuple<Exam, UserExamResult[]> GetReportData(int examId)
        {
            UserExamResult[] userExamResults = DataContext.Exams
                .Where(exam => exam.Id == examId)
                .SelectMany(exam => DataContext.UserExamResults.Where(result => result.Exam.Id == examId))
                .Include(result => result.ExamTestAnswers)
                .Include(result => result.User)
                .ToArray();

            return new Tuple<Exam, UserExamResult[]>(
                DataContext.Exams.Include(exam => exam.Preset).First(exam => exam.Id == examId),
                userExamResults);
        }
    }
}