using FoodingApp.Api.Dtos;
using FoodingApp.Library.Dtos;

namespace FoodingApp.Api.Services.Interfaces
{
    public interface ISearchRepository
    {
     Task<PagedResultDto<FoodItemDto>> GetFoodItemAsync(FoodSearchQueryDto searchQuery, CancellationToken ct);
    }
}