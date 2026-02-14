namespace FoodingApp.Library.Dtos
{
    public class FoodCategoryDto
    {
        public string GroupName { get; set; } = string.Empty;
        public int PrimaryCategoryId { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;
        public int SubCategoryId { get; set; }

        public FoodCategoryDto()
        {

        }


        // conversion from FoodCategory to FoodCategoryDto  

        public FoodCategoryDto(FoodCategory category)
        {
            GroupName = category.PrimaryGroup.Name;
            PrimaryCategoryId = category.PrimaryGroupId;
            SubCategoryName = category.SubCategory.Name;
            SubCategoryId = category.SubCategoryId;

        }


        public static explicit operator FoodCategoryDto(FoodCategory category) => new FoodCategoryDto(category);
    }
}
