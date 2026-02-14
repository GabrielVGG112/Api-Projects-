namespace FoodingApp.Library.Dtos
{
    public class FoodCategoryForManipulationDto
    {
        public FoodCategoryForManipulationDto()
        {

        }
       
        public int PrimaryGroupId { get; set; }
        public int SubCategoryId { get; set; }


        // conversion from FoodCategory to FoodCategoryForManipulationDto

        public FoodCategoryForManipulationDto(FoodCategory category)
        {
            PrimaryGroupId = category.PrimaryGroupId;
            SubCategoryId = category.SubCategoryId;

        }

        public static explicit operator FoodCategoryForManipulationDto(FoodCategory category) => new FoodCategoryForManipulationDto(category);
    }
}
