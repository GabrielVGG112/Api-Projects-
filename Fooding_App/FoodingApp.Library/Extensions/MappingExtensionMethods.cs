using FoodingApp.Api.Dtos;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using FoodingApp.Library.Nutrition;
using System.Reflection.Metadata.Ecma335;

namespace FoodingApp.Library.Extensions;

public static class MappingExtensionMethods
{

    public static IQueryable<FoodItemDto> ToFoodItemDto(this IQueryable<FoodItemModel> query)
    {
        return query.Select(fi => new FoodItemDto
        {    Id = fi.Id,
            ItemName = fi.ItemName,
            PrimaryCategoryId = fi.Category.PrimaryGroup.Id,
            SubCategoryId = fi.Category.SubCategory.Id,
        });
    }

    public static void UpdateVitaminsFromDto(this Vitamins entity ,VitaminsDto dto) 
    {
        entity.VitaminA = dto.VitaminA;
        entity.VitaminB1 = dto.VitaminB1;
        entity.VitaminB2 = dto.VitaminB2;
        entity.VitaminB3 = dto.VitaminB3;
        entity.VitaminB5 = dto.VitaminB5;
        entity.VitaminB6 = dto.VitaminB6;
        entity.VitaminB7 = dto.VitaminB7;
        entity.VitaminB9 = dto.VitaminB9;
        entity.VitaminB12 = dto.VitaminB12;
        entity.VitaminC = dto.VitaminC;
        entity.VitaminD = dto.VitaminD;
        entity.VitaminE = dto.VitaminE;
        entity.VitaminK = dto.VitaminK;
    }
    public static void UpdateMineralsFromDto(this Minerals entity ,MineralsDto dto) 
    {

        entity.Calcium = dto.Calcium;
        entity.Chloride = dto.Chloride;
        entity.Magnesium = dto.Magnesium;
        entity.Phosphorus = dto.Phosphorus;
        entity.Potassium = dto.Potassium;
        entity.Sodium = dto.Sodium;
        entity.Iron = dto.Iron;
        entity.Zinc = dto.Zinc;
        entity.Copper = dto.Copper;
        entity.Manganese = dto.Manganese;

    }
    public static void UpdateMacrosFromDto(this MacroNutrients entity,MacroNutrientsDto dto) 
    {


        entity.Carbs = dto.Carbs;
        entity.Sugars = dto.Sugars;
        entity.Fat = dto.Fat;
        entity.SaturatedFat = dto.SaturatedFat;
        entity.Protein = dto.Protein;
        entity.Fiber = dto.Fiber;
        entity.Alcohol = dto.Alcohol;
        entity.Water = dto.Water;

    }
    public static IQueryable<VitaminsDto> ToVitaminsDto(this IQueryable<FoodItemModel> query) 
    {
        return query.Select(fi => new VitaminsDto
        {
           FoodItemId = fi.Id,
            VitaminA = fi.Nutrients.Vitamins.VitaminA,
            VitaminB1 = fi.Nutrients.Vitamins.VitaminB1,
            VitaminB2 = fi.Nutrients.Vitamins.VitaminB2,
            VitaminB3 = fi.Nutrients.Vitamins.VitaminB3,
            VitaminB5 = fi.Nutrients.Vitamins.VitaminB5,
            VitaminB6 = fi.Nutrients.Vitamins.VitaminB6,
            VitaminB7 = fi.Nutrients.Vitamins.VitaminB7,
            VitaminB9 = fi.Nutrients.Vitamins.VitaminB9,
            VitaminB12 = fi.Nutrients.Vitamins.VitaminB12,
            VitaminC = fi.Nutrients.Vitamins.VitaminC,
            VitaminD = fi.Nutrients.Vitamins.VitaminD,
            VitaminE = fi.Nutrients.Vitamins.VitaminE,
            VitaminK = fi.Nutrients.Vitamins.VitaminK
        });
    }
    public static IQueryable<MacroNutrientsDto> ToMacroNutrientsDto(this IQueryable<FoodItemModel> query)
    {
        return query.Select(fi => new MacroNutrientsDto
        {
            FoodItemId = fi.Id,
            Carbs = fi.Nutrients.Macros.Carbs,
            Sugars = fi.Nutrients.Macros.Sugars,
            Fat = fi.Nutrients.Macros.Fat,
            SaturatedFat = fi.Nutrients.Macros.SaturatedFat,
            Protein = fi.Nutrients.Macros.Protein,
            Fiber = fi.Nutrients.Macros.Fiber,
            Alcohol = fi.Nutrients.Macros.Alcohol,
            Water = fi.Nutrients.Macros.Water
        });
    }
    public static IQueryable<MineralsDto> ToMineralsDto(this IQueryable<FoodItemModel> query)
    {
        return query.Select(fi => new MineralsDto
        {
            FoodItemId = fi.Id,
            Calcium = fi.Nutrients.Minerals.Calcium,
            Chloride = fi.Nutrients.Minerals.Chloride,
            Magnesium = fi.Nutrients.Minerals.Magnesium,
            Phosphorus = fi.Nutrients.Minerals.Phosphorus,
            Potassium = fi.Nutrients.Minerals.Potassium,
            Sodium = fi.Nutrients.Minerals.Sodium,
            Iron = fi.Nutrients.Minerals.Iron,
            Zinc = fi.Nutrients.Minerals.Zinc,
            Copper = fi.Nutrients.Minerals.Copper,
            Manganese = fi.Nutrients.Minerals.Manganese
        });
    }
    public static FoodItemModel ToEnityModel(this FoodItemForManipulationDto dto)
    {
        return new FoodItemModel()
        {
            CategoryId = dto.CategoryId,
            ItemName = dto.ItemName,
            Nutrients = new Nutrients
            {
                Macros = new MacroNutrients
                {
                    Carbs = dto.Nutrients.Macros.Carbs,
                    Sugars = dto.Nutrients.Macros.Sugars,
                    Fat = dto.Nutrients.Macros.Fat,
                    SaturatedFat = dto.Nutrients.Macros.SaturatedFat,
                    Protein = dto.Nutrients.Macros.Protein,
                    Fiber = dto.Nutrients.Macros.Fiber,
                    Alcohol = dto.Nutrients.Macros.Alcohol,
                    Water = dto.Nutrients.Macros.Water
                },
                Vitamins = new Vitamins
                {
                    VitaminA = dto.Nutrients.Vitamins.VitaminA,
                    VitaminB1 = dto.Nutrients.Vitamins.VitaminB1,
                    VitaminB2 = dto.Nutrients.Vitamins.VitaminB2,
                    VitaminB3 = dto.Nutrients.Vitamins.VitaminB3,
                    VitaminB5 = dto.Nutrients.Vitamins.VitaminB5,
                    VitaminB6 = dto.Nutrients.Vitamins.VitaminB6,
                    VitaminB7 = dto.Nutrients.Vitamins.VitaminB7,
                    VitaminB9 = dto.Nutrients.Vitamins.VitaminB9,
                    VitaminB12 = dto.Nutrients.Vitamins.VitaminB12,
                    VitaminC = dto.Nutrients.Vitamins.VitaminC,
                    VitaminD = dto.Nutrients.Vitamins.VitaminD,
                    VitaminE = dto.Nutrients.Vitamins.VitaminE,
                    VitaminK = dto.Nutrients.Vitamins.VitaminK
                },
                Minerals = new Minerals
                {
                    Calcium = dto.Nutrients.Minerals.Calcium,
                    Chloride = dto.Nutrients.Minerals.Chloride,
                    Magnesium = dto.Nutrients.Minerals.Magnesium,
                    Phosphorus = dto.Nutrients.Minerals.Phosphorus,
                    Potassium = dto.Nutrients.Minerals.Potassium,
                    Sodium = dto.Nutrients.Minerals.Sodium,
                    Iron = dto.Nutrients.Minerals.Iron,
                    Zinc = dto.Nutrients.Minerals.Zinc,
                    Copper = dto.Nutrients.Minerals.Copper,
                    Manganese = dto.Nutrients.Minerals.Manganese
                }
            }
        };
    }

    public static FoodItemForManipulationDto ToManipulationDto(this FoodItemModel model)
    {
        return new FoodItemForManipulationDto
        {
            CategoryId = model.CategoryId,
            ItemName = model.ItemName,
            Nutrients = new NutrientsDto
            {
                Macros = new MacroNutrientsDto
                {
                    Carbs = model.Nutrients.Macros.Carbs,
                    Sugars = model.Nutrients.Macros.Sugars,
                    Fat = model.Nutrients.Macros.Fat,
                    SaturatedFat = model.Nutrients.Macros.SaturatedFat,
                    Protein = model.Nutrients.Macros.Protein,
                    Fiber = model.Nutrients.Macros.Fiber,
                    Alcohol = model.Nutrients.Macros.Alcohol,
                    Water = model.Nutrients.Macros.Water
                },
                Vitamins = new VitaminsDto
                {
                    VitaminA = model.Nutrients.Vitamins.VitaminA,
                    VitaminB1 = model.Nutrients.Vitamins.VitaminB1,
                    VitaminB2 = model.Nutrients.Vitamins.VitaminB2,
                    VitaminB3 = model.Nutrients.Vitamins.VitaminB3,
                    VitaminB5 = model.Nutrients.Vitamins.VitaminB5,
                    VitaminB6 = model.Nutrients.Vitamins.VitaminB6,
                    VitaminB7 = model.Nutrients.Vitamins.VitaminB7,
                    VitaminB9 = model.Nutrients.Vitamins.VitaminB9,
                    VitaminB12 = model.Nutrients.Vitamins.VitaminB12,
                    VitaminC = model.Nutrients.Vitamins.VitaminC,
                    VitaminD = model.Nutrients.Vitamins.VitaminD,
                    VitaminE = model.Nutrients.Vitamins.VitaminE,
                    VitaminK = model.Nutrients.Vitamins.VitaminK
                },
                Minerals = new MineralsDto
                {
                    Calcium = model.Nutrients.Minerals.Calcium,
                    Chloride = model.Nutrients.Minerals.Chloride,
                    Magnesium = model.Nutrients.Minerals.Magnesium,
                    Phosphorus = model.Nutrients.Minerals.Phosphorus,
                    Potassium = model.Nutrients.Minerals.Potassium,
                    Sodium = model.Nutrients.Minerals.Sodium,
                    Iron = model.Nutrients.Minerals.Iron,
                    Zinc = model.Nutrients.Minerals.Zinc,
                    Copper = model.Nutrients.Minerals.Copper,
                    Manganese = model.Nutrients.Minerals.Manganese
                }
            }
        };
    }


    public static void UpdateFromDto(this FoodItemModel model, FoodItemForManipulationDto dto)
    {
        model.CategoryId = dto.CategoryId;
        model.ItemName = dto.ItemName;


        model.Nutrients.Macros.Carbs = dto.Nutrients.Macros.Carbs;
        model.Nutrients.Macros.Sugars = dto.Nutrients.Macros.Sugars;
        model.Nutrients.Macros.Fat = dto.Nutrients.Macros.Fat;
        model.Nutrients.Macros.SaturatedFat = dto.Nutrients.Macros.SaturatedFat;
        model.Nutrients.Macros.Protein = dto.Nutrients.Macros.Protein;
        model.Nutrients.Macros.Fiber = dto.Nutrients.Macros.Fiber;
        model.Nutrients.Macros.Alcohol = dto.Nutrients.Macros.Alcohol;
        model.Nutrients.Macros.Water = dto.Nutrients.Macros.Water;


        model.Nutrients.Vitamins.VitaminA = dto.Nutrients.Vitamins.VitaminA;
        model.Nutrients.Vitamins.VitaminB1 = dto.Nutrients.Vitamins.VitaminB1;
        model.Nutrients.Vitamins.VitaminB2 = dto.Nutrients.Vitamins.VitaminB2;
        model.Nutrients.Vitamins.VitaminB3 = dto.Nutrients.Vitamins.VitaminB3;
        model.Nutrients.Vitamins.VitaminB5 = dto.Nutrients.Vitamins.VitaminB5;
        model.Nutrients.Vitamins.VitaminB6 = dto.Nutrients.Vitamins.VitaminB6;
        model.Nutrients.Vitamins.VitaminB7 = dto.Nutrients.Vitamins.VitaminB7;
        model.Nutrients.Vitamins.VitaminB9 = dto.Nutrients.Vitamins.VitaminB9;
        model.Nutrients.Vitamins.VitaminB12 = dto.Nutrients.Vitamins.VitaminB12;
        model.Nutrients.Vitamins.VitaminC = dto.Nutrients.Vitamins.VitaminC;
        model.Nutrients.Vitamins.VitaminD = dto.Nutrients.Vitamins.VitaminD;
        model.Nutrients.Vitamins.VitaminE = dto.Nutrients.Vitamins.VitaminE;
        model.Nutrients.Vitamins.VitaminK = dto.Nutrients.Vitamins.VitaminK;


        model.Nutrients.Minerals.Calcium = dto.Nutrients.Minerals.Calcium;
        model.Nutrients.Minerals.Chloride = dto.Nutrients.Minerals.Chloride;
        model.Nutrients.Minerals.Magnesium = dto.Nutrients.Minerals.Magnesium;
        model.Nutrients.Minerals.Phosphorus = dto.Nutrients.Minerals.Phosphorus;
        model.Nutrients.Minerals.Potassium = dto.Nutrients.Minerals.Potassium;
        model.Nutrients.Minerals.Sodium = dto.Nutrients.Minerals.Sodium;
        model.Nutrients.Minerals.Iron = dto.Nutrients.Minerals.Iron;
        model.Nutrients.Minerals.Zinc = dto.Nutrients.Minerals.Zinc;
        model.Nutrients.Minerals.Copper = dto.Nutrients.Minerals.Copper;
        model.Nutrients.Minerals.Manganese = dto.Nutrients.Minerals.Manganese;
    }



    public static IQueryable<FoodCategoryDto> ToCategoryListDto(this IQueryable<FoodCategory> categories)
    {
        return categories.Select(fc => new FoodCategoryDto
        {
            PrimaryCategoryId = fc.PrimaryGroupId,
            SubCategoryId = fc.SubCategoryId,
            GroupName = fc.PrimaryGroup.Name,
            SubCategoryName = fc.SubCategory.Name
        });
    }

    public static FoodCategoryDto? ToCategoryDto(this FoodCategory entity)
    {
        return new FoodCategoryDto
        {
            PrimaryCategoryId = entity.PrimaryGroupId,
            SubCategoryId = entity.SubCategoryId,
            GroupName = entity.PrimaryGroup.Name,
            SubCategoryName = entity.SubCategory.Name
        };
    }

    public static FoodCategory ToEntity(this FoodCategoryForManipulationDto dto)
    {
        return new FoodCategory { SubCategoryId = dto.SubCategoryId, PrimaryGroupId = dto.PrimaryGroupId };
    }

    public static void UpdateFoodCategory(this FoodCategory entity, FoodCategoryForManipulationDto dto) 
    {
     
        
    }
}