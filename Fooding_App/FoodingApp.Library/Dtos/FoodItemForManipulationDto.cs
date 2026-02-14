using FoodingApp.Library.Models;

namespace FoodingApp.Library.Dtos
{
    public class FoodItemForManipulationDto
    {
        public int CategoryId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public NutrientsDto Nutrients { get; set; }


        public FoodItemForManipulationDto()
        {

        }
        public FoodItemForManipulationDto(FoodItemModel model)
        {
            CategoryId = model.CategoryId;
            ItemName = model.ItemName;
            Nutrients =(NutrientsDto) model.Nutrients;
        }

        public static explicit operator FoodItemForManipulationDto(FoodItemModel model) => new FoodItemForManipulationDto(model);   
    }
}
