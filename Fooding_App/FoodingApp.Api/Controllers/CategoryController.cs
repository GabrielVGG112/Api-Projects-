using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.Library.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.Api.Controllers
{// TO DO : refactor : Middleware  For Exception Handling
    // TO DO : Fluent Validation
    // Logging
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]  
        public async Task<ActionResult<IEnumerable<FoodCategoryDto>>> GetAllAsync(CancellationToken ct)
        {
            var categories = await _repo.GetAllAsync(ct);
            return Ok(categories);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FoodCategoryDto>> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category is not null)

                return Ok(category);

            else

                return NotFound();
        }

        [HttpGet("{id}/fooditems")]
        public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetFoodItemsByCategoryIdAsync(int id)
        {
           
                var items = await _repo.GetFoodItemsFromCategoryIdAsync(id);
                return Ok(items);
        
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
        
              await  _repo.SoftDeleteAsync(id);
                return NoContent();
            
        
        }


        [HttpPost]
        public async Task<IActionResult> PostCategory(FoodCategoryForManipulationDto category)
        {

            var item = await _repo.AddAsync(category);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PostCategory(int id, FoodCategoryForManipulationDto category)
        {

             await _repo.UpdateAsync(id, category);
             return NoContent();

        
        }

    }


 

}
