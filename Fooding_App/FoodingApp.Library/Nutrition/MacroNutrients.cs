using FoodingApp.Api.Dtos;

namespace FoodingApp.Library.Nutrition;

public class MacroNutrients
{

    public double Carbs { get; set; }
    public double Sugars { get; set; }
    public double Fat { get; set; }
    public double SaturatedFat { get; set; }
    public double Protein { get; set; }
    public double Fiber { get; set; }
    public double Alcohol { get; set; }
    public double Water { get; set; }

    public MacroNutrients()
    {

    }
    public MacroNutrients(MacroNutrientsDto dto)
    {
        Carbs = dto.Carbs;
        Sugars = dto.Sugars;
        Fat = dto.Fat;
        SaturatedFat = dto.SaturatedFat;
        Protein = dto.Protein;
        Fiber = dto.Fiber;
        Alcohol = dto.Alcohol;
        Water = dto.Water;
    }
    public static explicit operator MacroNutrients(MacroNutrientsDto dto) => new MacroNutrients(dto);
}