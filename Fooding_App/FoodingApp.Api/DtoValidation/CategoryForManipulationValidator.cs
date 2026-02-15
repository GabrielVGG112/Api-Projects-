using FluentValidation;
using FoodingApp.Library.Dtos;

namespace FoodingApp.Api.DtoValidation
{
    public class CategoryForManipulationValidator :AbstractValidator<FoodCategoryForManipulationDto>
    {
        public CategoryForManipulationValidator()
        {
            RuleFor(fc => fc.PrimaryGroupId).NotEmpty().NotNull().NotEqual(0).WithMessage("Primary group id can't be 0 null or empty");
            RuleFor(fc => fc.SubCategoryId).NotEmpty().NotNull().NotEqual(0).WithMessage("Subgroup id can't be 0 null or empty");
        }
    }
}
