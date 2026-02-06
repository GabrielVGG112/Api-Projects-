using AutoMapper;
using FoodingApp.Api.Dtos;
using FoodingApp.Library;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using FoodingApp.Library.Nutrition;

namespace FoodingApp.Api.Services.Mapping
{
    public class ItemsAndCategoriesMapper : Profile
    {
        public ItemsAndCategoriesMapper()
        {
            CreateMap<FoodItemModel, FoodItemDto>()
               .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.Category.SubCategory.Id))
               .ForMember(dest => dest.PrimaryCategoryId, opt => opt.MapFrom(src => src.Category.PrimaryGroup.Id));

            CreateMap<FoodItemModel, FoodItemForManipulationDto>()
                .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.Nutrients)).ReverseMap();

            CreateMap<VitaminsDto, Vitamins>().ReverseMap();
            CreateMap<Minerals, MineralsDto>().ReverseMap();
            CreateMap<MacroNutrients, MacroNutrientsDto>().ReverseMap();


            CreateMap<FoodCategory, FoodCategoryDto>()
                .ForMember(dest => dest.PrimaryCategoryId, opt => opt.MapFrom(src => src.PrimaryGroupId))
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.PrimaryGroup.Name))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.Name));

            CreateMap<FoodCategoryDto, FoodCategory>()
                .ForMember(dest => dest.PrimaryGroupId, opt => opt.MapFrom(src => src.PrimaryCategoryId))
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
                .ForMember(dest => dest.PrimaryGroup, opt => opt.Ignore())
                .ForMember(dest => dest.SubCategory, opt => opt.Ignore());

            CreateMap<FoodCategoryForManipulationDto, FoodCategory>()
                 .ForMember(dest => dest.PrimaryGroupId, opt => opt.MapFrom(src => src.PrimaryGroupId))
                 .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId));

            CreateMap<FoodCategory, FoodCategoryForManipulationDto>()
                 .ForMember(dest => dest.PrimaryGroupId, opt => opt.MapFrom(src => src.PrimaryGroupId))
                 .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId));




        }
    }
}
