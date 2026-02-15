using FluentValidation;
using FoodingApp.Library.Dtos;

namespace FoodingApp.Api.DtoValidation
{
    public class FoodItemForManipulationDtoValidator :AbstractValidator<FoodItemForManipulationDto>
    {

        public FoodItemForManipulationDtoValidator()
        {
            RuleFor(fi => fi.ItemName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("The food item name is required and should be between 3 and 30 char long");
                ;

            RuleFor(fi => fi.Nutrients).NotNull().WithMessage("the Nutrients fields are required");
           
            When(fi => fi.Nutrients is not null, () => 
            {
                RuleFor(fi=>fi.Nutrients.Vitamins)
                .NotNull()
                .WithMessage("the Vitamins fields are required"); ;
                
                RuleFor(fi=>fi.Nutrients.Macros)
                .NotNull()
                .WithMessage("the Macros fields are required"); ;
               
                RuleFor(fi=>fi.Nutrients.Minerals)
                .NotNull()
                .WithMessage("the Minerals fields are required"); ;
            });
            
            RuleFor(fi => fi.CategoryId).NotNull().NotEmpty().NotEqual(0)
                .WithMessage("The Category id cant be null empty or 0");
        }

    }
}
