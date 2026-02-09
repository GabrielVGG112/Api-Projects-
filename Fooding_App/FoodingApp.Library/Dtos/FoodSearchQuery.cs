namespace FoodingApp.Library.Dtos
{
    public class FoodSearchQueryDto
    {
        public string? Name { get; set; }
        public int? PrimaryCategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int Page { get; set; } 
        public int PageSize { get; set; }
        public string? SortBy { get; set; } 
        public bool Desc { get; set; } = false;
    }

}
