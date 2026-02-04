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
        public async Task<ActionResult<IEnumerable<FoodCategoryDto>>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
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
            try
            {
                var items = await _repo.GetFoodItemsFromCategoryIdAsync(id);
                return Ok(items);
            }
            catch (CategoryException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
              await  _repo.SoftDeleteAsync(id);
                return NoContent();
            }
            catch (CategoryException e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostCategory(FoodCategoryForManipulationDto category)
        {
            try
            {
                var item = await _repo.AddAsync(category);

                return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException?.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase)is true)
                {

                  return Conflict("A category with primary and secondary key exists alredy");

                }


                return BadRequest("Could not create the food item due to a database error.");
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PostCategory(int id, FoodCategoryForManipulationDto category)
        {
           
            try
            {

             await _repo.UpdateAsync(id, category);
             return NoContent();

            }

            catch (CategoryException e) 
            {

                return BadRequest(e.Message);

            }
        }

    }


 

}
