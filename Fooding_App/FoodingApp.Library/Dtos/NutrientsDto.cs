using FoodingApp.Api.Dtos;

namespace FoodingApp.Library.Dtos
{
    public class NutrientsDto
    {
        public MacroNutrientsDto Macros { get; set; } = new();
        public VitaminsDto Vitamins { get; set; } = new();
        public MineralsDto Minerals { get; set; } = new();
    }
}