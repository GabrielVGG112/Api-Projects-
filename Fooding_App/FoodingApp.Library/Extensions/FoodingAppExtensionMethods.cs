using FoodingApp.Library.Enums;
using FoodingApp.Library.Nutrition;
using System.Numerics;

namespace FoodingApp.Library.Extensions;

public static class FoodingAppExtensionMethods
{
    public static T ConvertToUnit<T>(this T value, UnitEnum unit)
        where T : INumber<T>

        => unit switch
        {
            UnitEnum.Gram => value,
            UnitEnum.Milliliter => value,
            UnitEnum.Teaspoon => value * T.CreateChecked(5),
            UnitEnum.Tablespoon => value * T.CreateChecked(15),
            UnitEnum.Cup => value * T.CreateChecked(240),
            _ => throw new NotImplementedException()
        };



    public static double CalculateKj(this MacroNutrients macro) => macro.CalculateKcalFor(100) * 4.184;
    public static double CalculateKjFor(this MacroNutrients macro, double grams) => macro.CalculateKcalFor(grams) * 4.184;



    public static double CalculateKcalFor(this MacroNutrients macros, double grams)
    {
        (double proteinKcal, double carbsKcal) = macros.ProteinCarbsKcalFor(grams);

        return proteinKcal + carbsKcal + macros.FatKcalFor(grams) + macros.AlcoholKcalFor(grams);
    }


    public static NutriScoreEnum CalculateNutriScore(this Nutrients n)
  => NutriScorePointsHelper.Calculate(n);



    #region private methods
    private static (double proteinKcal, double carbsKcal) ProteinCarbsKcalFor(this MacroNutrients macros, double grams)
    {
        return (4 * (macros.Protein / 100) * grams, 4 * (macros.Carbs / 100) * grams);
    }

    private static double FatKcalFor(this MacroNutrients macros, double grams)
        => 9 * macros.Fat / 100 * grams;

    public static double AlcoholKcalFor(this MacroNutrients macros, double grams)
        => 7 * macros.Alcohol / 100 * grams;


    #endregion


}
