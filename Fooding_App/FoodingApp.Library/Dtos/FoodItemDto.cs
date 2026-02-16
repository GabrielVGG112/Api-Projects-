using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;

namespace FoodingApp.Api.Dtos
{
    public class FoodItemDto
    {
  
        public string ItemName { get; set; }
        public int PrimaryCategoryId { get; set; }
        public int SubCategoryId { get; set; }


        public FoodItemDto()
        {

        }
        public FoodItemDto(FoodItemModel entity)
        {
            ItemName = entity.ItemName;
            PrimaryCategoryId = entity.Category.PrimaryGroupId;
            SubCategoryId = entity.Category.SubCategoryId;

        }

        public static explicit operator FoodItemDto(FoodItemModel entity) => new FoodItemDto(entity);
    }
}
public class FoodItemForPatchDto 
{
    public int CategoryId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public NutrientsDto Nutrients { get; set; }


    public FoodItemForPatchDto()
    {

    }
    public FoodItemForPatchDto(FoodItemModel model)
    {
        CategoryId = model.CategoryId;
        ItemName = model.ItemName;
        Nutrients = (NutrientsDto)model.Nutrients;
    }

    public static explicit operator FoodItemForPatchDto(FoodItemModel model) => new FoodItemForPatchDto(model);
}