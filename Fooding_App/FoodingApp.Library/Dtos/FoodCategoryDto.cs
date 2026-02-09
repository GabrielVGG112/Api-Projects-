namespace FoodingApp.Library.Dtos
{
    public class FoodCategoryDto
    {
        public string GroupName { get; set; } = string.Empty;
        public int PrimaryCategoryId { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;
        public int SubCategoryId { get; set; }
    }
}
