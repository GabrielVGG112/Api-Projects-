using FoodingApp.Library.Models;
using FoodingApp.Library.Nutrition;

namespace FoodingApp.Api.Dtos
{
    public class VitaminsDto
    {
        public int FoodItemId { get; set; }
        public double VitaminA { get; set; }
        public double VitaminB1 { get; set; }
        public double VitaminB2 { get; set; }
        public double VitaminB3 { get; set; }
        public double VitaminB5 { get; set; }
        public double VitaminB6 { get; set; }
        public double VitaminB7 { get; set; }
        public double VitaminB9 { get; set; }
        public double VitaminB12 { get; set; }
        public double VitaminC { get; set; }
        public double VitaminD { get; set; }
        public double VitaminE { get; set; }
        public double VitaminK { get; set; }


        public VitaminsDto()
        {

        }

        public VitaminsDto(FoodItemModel model)
        {
            FoodItemId = model.Id;
            VitaminA = model.Nutrients.Vitamins.VitaminA;
            VitaminB1 = model.Nutrients.Vitamins.VitaminB1;
            VitaminB2 = model.Nutrients.Vitamins.VitaminB2;
            VitaminB3 = model.Nutrients.Vitamins.VitaminB3;
            VitaminB5 = model.Nutrients.Vitamins.VitaminB5;
            VitaminB6 = model.Nutrients.Vitamins.VitaminB6;
            VitaminB7 = model.Nutrients.Vitamins.VitaminB7;
            VitaminB9 = model.Nutrients.Vitamins.VitaminB9;
            VitaminB12 = model.Nutrients.Vitamins.VitaminB12;
            VitaminC = model.Nutrients.Vitamins.VitaminC;
            VitaminD = model.Nutrients.Vitamins.VitaminD;
            VitaminE = model.Nutrients.Vitamins.VitaminE;
            VitaminK = model.Nutrients.Vitamins.VitaminK;

        }

        public VitaminsDto(Vitamins vitamins)
        {
            VitaminA = vitamins.VitaminA;
            VitaminB1 = vitamins.VitaminB1;
            VitaminB2 = vitamins.VitaminB2;
            VitaminB3 = vitamins.VitaminB3;
            VitaminB5 = vitamins.VitaminB5;
            VitaminB6 = vitamins.VitaminB6;
            VitaminB7 = vitamins.VitaminB7;
            VitaminB9 = vitamins.VitaminB9;
            VitaminB12 = vitamins.VitaminB12;
            VitaminC = vitamins.VitaminC;
            VitaminD = vitamins.VitaminD;
            VitaminE = vitamins.VitaminE;
            VitaminK = vitamins.VitaminK;
        }
        public static explicit operator VitaminsDto(FoodItemModel model) => new VitaminsDto(model);
        public static explicit operator VitaminsDto(Vitamins vitamins) => new VitaminsDto(vitamins);
    }
}