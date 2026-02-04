using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriScoreCalculator;

public static class NutriScorePointsHelper
{
    public static NutriScoreEnum Calculate(Nutrients n)
    {
        int negative =
            PointsEnergy(n.Macros.CalculateKj()) +
            PointsSugar(n.Macros.Sugars) +
            PointsSatFat(n.Macros.SaturatedFat) +
            PointsSodium(n.Minerals.Sodium);

        int positive =
            PointsFiber(n.Macros.Fiber) +
            PointsProtein(n.Macros.Protein);

        int score = negative - positive;
        return ToEnum(score);
    }

    // --- Negative points ---
    private static int PointsEnergy(double kj) => kj switch
    {
        <= 335 => 0,
        <= 670 => 1,
        <= 1005 => 2,
        <= 1340 => 3,
        <= 1675 => 4,
        <= 2010 => 5,
        <= 2345 => 6,
        <= 2680 => 7,
        <= 3015 => 8,
        <= 3350 => 9,
        _ => 10
    };

    private static int PointsSugar(double g) => g switch
    {
        <= 4.5 => 0,
        <= 9 => 1,
        <= 13.5 => 2,
        <= 18 => 3,
        <= 22.5 => 4,
        <= 27 => 5,
        <= 31 => 6,
        <= 36 => 7,
        <= 40 => 8,
        <= 45 => 9,
        _ => 10
    };

    private static int PointsSatFat(double g) => g switch
    {
        <= 1 => 0,
        <= 2 => 1,
        <= 3 => 2,
        <= 4 => 3,
        <= 5 => 4,
        <= 6 => 5,
        <= 7 => 6,
        <= 8 => 7,
        <= 9 => 8,
        <= 10 => 9,
        _ => 10
    };

    private static int PointsSodium(double mg) => mg switch
    {
        <= 90 => 0,
        <= 180 => 1,
        <= 270 => 2,
        <= 360 => 3,
        <= 450 => 4,
        <= 540 => 5,
        <= 630 => 6,
        <= 720 => 7,
        <= 810 => 8,
        <= 900 => 9,
        _ => 10
    };

    // --- Positive points ---
    private static int PointsFiber(double g) => g switch
    {
        <= 0.9 => 0,
        <= 1.9 => 1,
        <= 2.8 => 2,
        <= 3.7 => 3,
        <= 4.7 => 4,
        _ => 5
    };

    private static int PointsProtein(double g) => g switch
    {
        <= 1.6 => 0,
        <= 3.2 => 1,
        <= 4.8 => 2,
        <= 6.4 => 3,
        <= 8.0 => 4,
        _ => 5
    };

    private static NutriScoreEnum ToEnum(int score) => score switch
    {
        <= -1 => NutriScoreEnum.A,
        <= 2 => NutriScoreEnum.B,
        <= 10 => NutriScoreEnum.C,
        <= 18 => NutriScoreEnum.D,
        _ => NutriScoreEnum.E
    };
}

