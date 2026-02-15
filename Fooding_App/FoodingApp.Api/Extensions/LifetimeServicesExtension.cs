using FluentValidation;
using FoodingApp.Api.DtoValidation;
using FoodingApp.Api.Services;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.EfCore.Services;
using FoodingApp.Library.Dtos;

namespace FoodingApp.Api.Extensions
{
    public static class LifetimeServicesExtension
    {
        public static IServiceCollection AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IValidator<FoodItemForManipulationDto>, FoodItemForManipulationDtoValidator>();
            services.AddScoped<IValidator<FoodCategoryForManipulationDto>, CategoryForManipulationValidator>();
            services.AddScoped<IFoodingItemRepository, FoodingItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISearchRepository , FoodingAppSearchRepository>();
            return services;
        }
    }
}
