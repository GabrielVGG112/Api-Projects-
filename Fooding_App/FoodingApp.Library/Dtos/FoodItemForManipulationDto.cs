namespace FoodingApp.Library.Dtos
{
    public class FoodItemForManipulationDto
    {
        public int CategoryId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public NutrientsDto Nutrients { get; set; }
    }
}
