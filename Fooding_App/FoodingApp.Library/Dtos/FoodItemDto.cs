using FoodingApp.Library.Models;

namespace FoodingApp.Api.Dtos
{
    public class FoodItemDto
    {
        public int Id { get; set; }
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
