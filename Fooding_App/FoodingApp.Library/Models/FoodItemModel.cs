using FoodingApp.Library.Dtos;
using FoodingApp.Library.Nutrition;

namespace FoodingApp.Library.Models;

public class FoodItemModel
{

    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public FoodCategory Category { get; set; }

    public Nutrients Nutrients { get; set; }

    public FoodItemModel()
    {

    }
    public FoodItemModel(FoodItemForManipulationDto dto)
    {
        CategoryId = dto.CategoryId;
        ItemName = dto.ItemName;
        Nutrients = (Nutrients)dto.Nutrients;
    }
    public static explicit operator  FoodItemModel (FoodItemForManipulationDto dto) => new FoodItemModel(dto);
}
