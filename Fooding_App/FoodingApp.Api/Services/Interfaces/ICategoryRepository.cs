using FoodingApp.Api.Dtos;
using FoodingApp.Library;
using FoodingApp.Library.Dtos;

namespace FoodingApp.Api.Services.Interfaces
{
    public interface ICategoryRepository : IRepository<FoodCategory, FoodCategoryDto, FoodCategoryForManipulationDto>
    {
       Task<IEnumerable<FoodCategoryDto>> GetPagedAsync(int page, int pageSize);
        Task<IEnumerable<FoodItemDto>> GetFoodItemsFromCategoryIdAsync(int categoryId);
    }
}