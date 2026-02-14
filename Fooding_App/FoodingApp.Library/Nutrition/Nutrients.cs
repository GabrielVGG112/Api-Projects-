using FoodingApp.Library.Dtos;
using System.Numerics;

namespace FoodingApp.Library.Nutrition;

public class Nutrients
{
    public MacroNutrients Macros { get; set; } = new();
    public Vitamins Vitamins { get; set; } = new();
    public Minerals Minerals { get; set; } = new();
    public Nutrients()
    {

    }
    public Nutrients(NutrientsDto dto)
    {
        Macros = (MacroNutrients)dto.Macros;
        Vitamins = (Vitamins)dto.Vitamins;
        Minerals = (Minerals)dto.Minerals;
    }
    public static explicit operator Nutrients(NutrientsDto dto) => new Nutrients(dto);
}
