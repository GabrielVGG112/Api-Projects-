namespace FoodingApp.Library.Dtos
{
    public class PagedResultDto<T> 
    {
        public IEnumerable<T> items { get; set; } = Enumerable.Empty<T>();
        public int Count { get; set; }  
        public int Page { get; set; }
        public int PageSize { get; set; } 
    }

}
