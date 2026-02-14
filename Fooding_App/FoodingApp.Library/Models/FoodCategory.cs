using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;

namespace FoodingApp.Library;



public class FoodCategory
{
    public int Id { get; set; }

    public int PrimaryGroupId { get; set; }
    public PrimaryCategory PrimaryGroup { get; set; }

    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
    public ICollection<FoodItemModel> FoodItems { get; set; } = new HashSet<FoodItemModel>();

    public FoodCategory()
    {

    }

    public FoodCategory(FoodCategoryDto dto)
    {
         PrimaryGroupId = dto.PrimaryCategoryId;
   
     
        SubCategoryId = dto.SubCategoryId;

    }
    public FoodCategory(FoodCategoryForManipulationDto dto)
    {
        PrimaryGroupId = dto.PrimaryGroupId;
        SubCategoryId = dto.SubCategoryId;
    }
    public static explicit operator FoodCategory(FoodCategoryDto dto)=>  new FoodCategory(dto);
    public static explicit operator FoodCategory(FoodCategoryForManipulationDto dto) => new FoodCategory(dto);
 
}



