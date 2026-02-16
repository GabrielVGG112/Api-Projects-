using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace FoodingApp.Api.Controllers
{
    // TO DO : Fluent Validation
    // Logging
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoriesController(ICategoryRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodCategoryDto>>> GetAllAsync(CancellationToken ct)
        {
            var categories = await _repo.GetAllAsync(ct);
            return Ok(categories);
        }
        [HttpGet("primary")]
        public async Task<ActionResult<IEnumerable<PrimaryCategory>>> GetAllPrimary(CancellationToken ct) 
        {
            var categories = await _repo.GeAllPrimaryCategoriesAsync(ct);
            return Ok(categories);
        }
        [HttpGet("secondary")]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetAllSubcategory(CancellationToken ct)
        {
            var categories = await _repo.GetAllSubCategoriesAsync(ct);
            return Ok(categories);
        }
        [HttpGet("/primary/{id}/secondary")]
        public async Task<ActionResult<IEnumerable<FoodCategoryDto>>> GetAllSubByPrimary(int id, CancellationToken ct)
        {
            var categories = await _repo.GetAllSubcategoriesFromOnePrimaryAsync(id,ct);
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {

            await _repo.SoftDeleteAsync(id);
            return NoContent();


        }
        [HttpGet("{categoryId}/fooditems/{foodItemId}")]
        public async Task<ActionResult<FoodItemDto>> GetSingleFoodItemFromCategoryId(int categoryId , int foodItemId) 
        {
            FoodItemDto dto =await _repo.GetSingleFoodItemFromCategoryAsync(categoryId , foodItemId);
            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult> PostCategory(FoodCategoryForManipulationDto category)
        {

            var item = await _repo.AddAsync(category);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, FoodCategoryForManipulationDto category)
        {

            await _repo.UpdateAsync(id, category);
            return NoContent();


        }

    }




}
