using FoodingApp.Api.Services.Interfaces;

namespace FoodingApp.Api.Services
{
    public class FoodingAppRepoFacade
    {
        public FoodingAppRepoFacade(ICategoryRepository categoryRepo, IFoodingItemRepository fItemRepo)
        {
            CategoryRepository = categoryRepo;
            FoodingItemRepository = fItemRepo;
        }
        public ICategoryRepository CategoryRepository { get; set; }
        public IFoodingItemRepository FoodingItemRepository { get; set; }
    }
}
