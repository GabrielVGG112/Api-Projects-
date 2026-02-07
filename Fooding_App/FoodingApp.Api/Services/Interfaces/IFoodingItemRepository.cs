using FoodingApp.Api.Dtos;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;

namespace FoodingApp.Api.Services.Interfaces
{
    public interface IFoodingItemRepository : IRepository<FoodItemModel, FoodItemDto, FoodItemForManipulationDto>
    {
        Task<IEnumerable<FoodItemDto>> GetPagedAsync(int page, int pageSize);
        Task<VitaminsDto> GetVitaminsAsync(int foodItemId);
        Task<MacroNutrientsDto> GetMacrosAsync(int foodItemId);
        Task<MineralsDto> GetMineralsAsync(int foodItemId);
        Task UpdateMineralsAsync(int id, MineralsDto dto);
        Task UpdateVitaminsAsync(int id, VitaminsDto dto);
        Task UpdateMacrosAsync(int id, MacroNutrientsDto dto);
        Task PatchDocumentAsync(int id, FoodItemDto dto);
    }
}