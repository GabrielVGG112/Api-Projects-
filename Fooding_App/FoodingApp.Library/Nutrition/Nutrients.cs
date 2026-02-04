namespace FoodingApp.Library.Nutrition;

public class Nutrients
{
    public MacroNutrients Macros { get; set; } = new();
    public Vitamins Vitamins { get; set; } = new();
    public Minerals Minerals { get; set; } = new();

}
