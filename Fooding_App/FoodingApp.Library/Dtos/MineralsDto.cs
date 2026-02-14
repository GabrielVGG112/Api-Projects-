using FoodingApp.Library.Models;
using FoodingApp.Library.Nutrition;

namespace FoodingApp.Api.Dtos
{
    public class MineralsDto
    {
        public int FoodItemId { get; set; }
        public double Calcium { get; set; }          // Ca
        public double Chloride { get; set; }         // Cl
        public double Magnesium { get; set; }        // Mg
        public double Phosphorus { get; set; }       // P
        public double Potassium { get; set; }        // K
        public double Sodium { get; set; }           // Na

        // Micro-minerals
        public double Iron { get; set; }             // Fe
        public double Zinc { get; set; }             // Zn
        public double Copper { get; set; }           // Cu
        public double Manganese { get; set; }        // Mn
        public double Selenium { get; set; }         // Se
        public double Iodine { get; set; }           // I
        public MineralsDto()
        {


        }
        // conversion constructor from FoodItemModel to MineralsDto
        

        public MineralsDto(FoodItemModel model)
        {
           FoodItemId = model.Id;
           Calcium = model.Nutrients.Minerals.Calcium;
           Chloride = model.Nutrients.Minerals.Chloride;
           Magnesium = model.Nutrients.Minerals.Magnesium;
           Phosphorus = model.Nutrients.Minerals.Phosphorus;
           Potassium = model.Nutrients.Minerals.Potassium;
           Sodium = model.Nutrients.Minerals.Sodium;
           Iron = model.Nutrients.Minerals.Iron;
           Zinc = model.Nutrients.Minerals.Zinc;
           Copper = model.Nutrients.Minerals.Copper;
           Manganese = model.Nutrients.Minerals.Manganese;
           Selenium = model.Nutrients.Minerals.Selenium;
           Iodine = model.Nutrients.Minerals.Iodine;
        }
        public MineralsDto(Minerals minerals)
        {
          Calcium = minerals.Calcium;
          Chloride = minerals.Chloride;
          Magnesium = minerals.Magnesium;
          Phosphorus = minerals.Phosphorus;
          Potassium = minerals.Potassium;
          Sodium = minerals.Sodium;
          Iron = minerals.Iron;
          Zinc = minerals.Zinc;
          Copper = minerals.Copper;
          Manganese = minerals.Manganese;
          Selenium = minerals.Selenium;
          Iodine = minerals.Iodine;
        }

       
        public static explicit operator MineralsDto(FoodItemModel model) => new MineralsDto(model); 
        public static explicit operator MineralsDto(Minerals minerals) => new MineralsDto(minerals);
    }
}