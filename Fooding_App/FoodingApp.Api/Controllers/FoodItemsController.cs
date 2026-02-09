using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoodingApp.Api.Controllers;
//
// TO DO : Fluent Validation


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
    public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetAll(CancellationToken ct)
    {

        return Ok(await _repo.GetAllAsync(ct));

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

        var createdItem = await _repo.AddAsync(foodItemDto);
        return CreatedAtAction(nameof(GetFoodItemByIdAsync), new { id = createdItem.Id }, createdItem);

    }




    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFoodItemAsync(int id, FoodItemForManipulationDto foodItemDto)
    {

        await _repo.UpdateAsync(id, foodItemDto);
        return NoContent();



    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletebyId(int id)
    {

        await _repo.SoftDeleteAsync(id);
        return NoContent();


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

        await _repo.PatchDocumentAsync(id, item);
        return NoContent();



    }


}



