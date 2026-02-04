using FoodingApp.Library.Models;

namespace FoodingApp.Library;



public class FoodCategory
{
    public int Id { get; set; }

    public int PrimaryGroupId { get; set; }
    public PrimaryCategory PrimaryGroup { get; set; }

    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
    public ICollection<FoodItemModel> FoodItems { get; set; } = new HashSet<FoodItemModel>();
}



