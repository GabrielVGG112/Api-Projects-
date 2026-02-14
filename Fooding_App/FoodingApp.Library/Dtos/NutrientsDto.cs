using FoodingApp.Api.Dtos;
using FoodingApp.Library.Nutrition;

namespace FoodingApp.Library.Dtos
{
    public class NutrientsDto
    {
        public MacroNutrientsDto Macros { get; set; } = new();
        public VitaminsDto Vitamins { get; set; } = new();
        public MineralsDto Minerals { get; set; } = new();
        public NutrientsDto()
        {

        }
        public NutrientsDto(Nutrients nutrients)
        {
            Macros = (MacroNutrientsDto)nutrients.Macros;
            Vitamins = (VitaminsDto)nutrients.Vitamins;
            Minerals = (MineralsDto)nutrients.Minerals;
        }


        public static explicit operator NutrientsDto(Nutrients nutrients) => new NutrientsDto(nutrients);
    }
}