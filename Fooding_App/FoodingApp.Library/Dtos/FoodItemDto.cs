namespace FoodingApp.Api.Dtos
{
    public class FoodItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; } 
        public int PrimaryCategoryId { get; set; }
        public int SubCategoryId{ get; set; } 

    }
}
