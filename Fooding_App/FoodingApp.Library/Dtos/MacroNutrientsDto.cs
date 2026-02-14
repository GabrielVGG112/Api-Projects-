using FoodingApp.Library.Models;
using FoodingApp.Library.Nutrition;
using System.Reflection;

namespace FoodingApp.Api.Dtos
{
    public class MacroNutrientsDto
    {
        public int FoodItemId { get; set; }
        public double Carbs { get; set; }
        public double Sugars { get; set; }
        public double Fat { get; set; }
        public double SaturatedFat { get; set; }
        public double Protein { get; set; }
        public double Fiber { get; set; }
        public double Alcohol { get; set; }
        public double Water { get; set; }
        public MacroNutrientsDto()
        {

        }
        public MacroNutrientsDto(FoodItemModel model)
        {
            FoodItemId = model.Id;
            Carbs = model.Nutrients.Macros.Carbs;
            Sugars = model.Nutrients.Macros.Sugars;
            Fat = model.Nutrients.Macros.Fat;
            SaturatedFat = model.Nutrients.Macros.SaturatedFat;
            Protein = model.Nutrients.Macros.Protein;
            Fiber = model.Nutrients.Macros.Fiber;
            Alcohol = model.Nutrients.Macros.Alcohol;
            Water = model.Nutrients.Macros.Water;
        }
        public MacroNutrientsDto(MacroNutrients model)
        {
            Carbs = model.Carbs;
            Sugars = model.Sugars;
            Fat = model.Fat;
            SaturatedFat = model.SaturatedFat;
            Protein = model.Protein;
            Fiber = model.Fiber;
            Alcohol = model.Alcohol;
            Water = model.Water;
        }
        public static explicit operator MacroNutrientsDto(FoodItemModel model) => new MacroNutrientsDto(model);
        public static explicit operator MacroNutrientsDto(MacroNutrients model) => new MacroNutrientsDto(model); 
    }
}