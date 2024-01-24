using System.Linq;
using usue_online_tests.Models;

namespace usue_online_tests.Helpers;

public class PredictionHelper
{
    private const string EyeLookInLeft = "eyeLookInLeft";
    private const string EyeLookInRight = "eyeLookInRight";

    public static bool IsEyesOpen(PredictionResult predictionResult)
    {
        var ebl = predictionResult.Categories.First(category => category.CategoryName == EyeLookInLeft);
        var ebr = predictionResult.Categories.First(category => category.CategoryName == EyeLookInRight);

        return ebl.Score < 0.4 && ebr.Score < 0.4;
    }
}
