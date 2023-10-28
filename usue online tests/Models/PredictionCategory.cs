using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace usue_online_tests.Models;

public class PredictionCategory
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; }

    public string DisplayName { get; set; }

    public int Index { get; set; }

    public double Score { get; set; }

    public Guid PredictionResultId { get; set; }

    [ForeignKey(nameof(PredictionResultId))]
    public PredictionResult PredictionResult { get; set; }
}