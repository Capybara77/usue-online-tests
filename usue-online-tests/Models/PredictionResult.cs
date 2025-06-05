using System;
using System.Collections.Generic;

namespace usue_online_tests.Models;

public class PredictionResult
{
    public Guid Id { get; set; }

    public int ExаmId { get; set; }

    public int UserId { get; set; }

    public bool IsCheating { get; set; }

    public int HeadIndex { get; set; }

    public string HeadName { get; set; }

    public DateTime Created { get; set; }

    public ICollection<PredictionCategory> Categories { get; set; }
}
