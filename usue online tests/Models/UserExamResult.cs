using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace usue_online_tests.Models;

public class UserExamResult
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public int ExamId { get; set; }

    [ForeignKey(nameof(ExamId))]
    public Exam Exam { get; set; }

    public DateTime DateTimeStart { get; set; }

    public ICollection<ExamTestAnswer> ExamTestAnswers { get; set; }

    public bool IsCompleted { get; set; }
}