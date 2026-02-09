using FoodingApp.Api.Dtos;
using FoodingApp.Library;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;

namespace FoodingApp.Api.Services.Interfaces
{
    public interface ICategoryRepository : IRepository<FoodCategory, FoodCategoryDto, FoodCategoryForManipulationDto>
    {
        Task<IEnumerable<FoodCategoryDto>> GetPagedAsync(int page, int pageSize);
        Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync(CancellationToken ct);
        Task<IEnumerable<FoodCategoryDto>> GetAllSubcategoriesFromOnePrimaryAsync(int id, CancellationToken ct);
        Task<IEnumerable<PrimaryCategory>> GeAllPrimaryCategoriesAsync(CancellationToken ct);
        Task<IEnumerable<FoodItemDto>> GetFoodItemsFromCategoryIdAsync(int categoryId);
    }
}