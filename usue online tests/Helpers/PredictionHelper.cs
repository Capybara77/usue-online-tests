using System.Linq;
using usue_online_tests.Models;

namespace usue_online_tests.Helpers;

public class PredictionHelper
{
    private const string EyeBlinkLeft = "eyeBlinkLeft";
    private const string EyeBlinkRight = "eyeBlinkRight";

    public static bool IsEyesOpen(PredictionResult predictionResult)
    {
        var ebl = predictionResult.Categories.First(category => category.CategoryName == EyeBlinkLeft);
        var ebr = predictionResult.Categories.First(category => category.CategoryName == EyeBlinkRight);

        return ebl.Score < 0.4 && ebr.Score < 0.4;
    }
}
