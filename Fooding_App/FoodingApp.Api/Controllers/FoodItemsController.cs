using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodingApp.Api.Controllers;
//TO DO : refactor : Middleware  For Exception Handling
// TO DO : Fluent Validation
// Logging

[Route("api/[controller]")]
[ApiController]
public class FoodItemsController : ControllerBase
{

    private readonly IFoodingItemRepository _repo;


    public FoodItemsController(IFoodingItemRepository repo)

    {
        _repo = repo;


    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetAll()
    {


        return Ok(await _repo.GetAllAsync());

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodItemDto>> GetFoodItemByIdAsync(int id)
    {


        var item = await _repo.GetByIdAsync(id);

        if (item is not null)
            return Ok(item);



        else
            return NotFound();

    }


    [HttpGet("paged")]
    public async Task<ActionResult<FoodItemModel>> GetPagedAsync([FromQuery] int page, [FromQuery] int pageSize)
    {


        return Ok(_repo.GetPagedAsync(page, pageSize));
    }



    [HttpPost]
    public async Task<IActionResult> CreateFoodItemAsync(FoodItemForManipulationDto foodItemDto)
    {
        try
        {
            var createdItem = await _repo.AddAsync(foodItemDto);
            return CreatedAtAction(nameof(GetFoodItemByIdAsync), new { id = createdItem.Id }, createdItem);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException?.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase) is true)
            {
                return Conflict("A category with primary and secondary key exists alredy");
            }

            if (e.InnerException?.Message.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase) is true)
            {
                return BadRequest("Invalid CategoryId or related entity does not exist.");
            }
            return BadRequest();
        }
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFoodItemAsync(int id, FoodItemForManipulationDto foodItemDto)
    {
        try
        {
            await _repo.UpdateAsync(id, foodItemDto);
            return NoContent();
        }
        catch (FoodItemException e)
        {
            return NotFound(e.Message);
        }

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletebyId(int id)
    {
        try
        {
            await _repo.SoftDeleteAsync(id);
            return NoContent();
        }
        catch (FoodItemException e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchFoodItem([FromBody] JsonPatchDocument<FoodItemDto> patchDocumet, int id)
    {
        FoodItemDto item = await _repo.GetByIdAsync(id);
        if (item is null)
        {
            return NotFound();
        }
        patchDocumet.ApplyTo(item, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _repo.PatchDocumentAsync(id, item);
            return NoContent();
        }
        catch (FoodItemException e)
        {

            return BadRequest(e.Message);
        }
        catch (CategoryException e)
        {
            return NotFound(e.Message);
        }

    }


}
    
   

