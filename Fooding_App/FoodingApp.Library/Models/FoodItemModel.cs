using FoodingApp.Library.Nutrition;

namespace FoodingApp.Library.Models;

public class FoodItemModel
{

    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public FoodCategory Category { get; set; }

    public Nutrients Nutrients { get; set; }

}
