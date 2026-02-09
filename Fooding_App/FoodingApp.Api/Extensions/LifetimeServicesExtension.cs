using FoodingApp.Api.Services;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.EfCore.Services;

namespace FoodingApp.Api.Extensions
{
    public static class LifetimeServicesExtension
    {
        public static IServiceCollection AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IFoodingItemRepository, FoodingItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<FoodingAppRepoFacade>();
            return services;
        }
    }
}
