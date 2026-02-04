using Microsoft.AspNetCore.JsonPatch;
using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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


    [HttpGet("{id}/vitamins")]
    public async Task<ActionResult<VitaminsDto>> GetVitamins(int id) 
    {
        try 
        {
            var vitamins = await _repo.GetVitaminsAsync(id);
            return Ok(vitamins);
        }
        catch (FoodItemException e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpGet("{id}/macros")]
    public async Task <ActionResult<MacroNutrientsDto>> GetMacroNutrients(int id)
    {
        try
        {
            var macros = await _repo.GetMacrosAsync(id);
            return Ok(macros);
        }
        catch (FoodItemException e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpGet("{id}/minerals")]
    public async Task<ActionResult<MineralsDto>> GetMinerals(int id)
    {
        try
        {
            var micros = await _repo.GetMineralsDtoAsync(id);
            return Ok(micros);
        }
        catch (FoodItemException e)
        {
            return NotFound(e.Message);
        }
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
                Conflict("A category with primary and secondary key exists alredy");
            }

            if (e.InnerException?.Message.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase) is true)
            {
                return BadRequest("Invalid CategoryId or related entity does not exist.");
            }
            return BadRequest();
        }
    }
    [HttpPatch("{id}/vitamins")] //
    public async Task<IActionResult>PatchVitamins(int id ,JsonPatchDocument<VitaminsDto> patchDocument) 
    {
        VitaminsDto dto = await  _repo.GetVitaminsAsync(id);
        if (dto is null) 
        {
            return NotFound();
        }
        patchDocument.ApplyTo(dto, ModelState);
        if (!ModelState.IsValid) 
        {
            return BadRequest(ModelState);
        }
        try
        {
            await UpdateVitamins(id, dto);
            return NoContent();
        }
        catch (FoodItemException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/vitamins")]
    public async Task<IActionResult> UpdateVitamins(int id,VitaminsDto dto) 
    {
        try
        {
            await _repo.UpdateVitaminsAsync(id, dto);
            return NoContent();
        }
        catch (FoodItemException e)
        {
          return  BadRequest(e.Message);
        }
    }
    [HttpPut("{id}/minerals")]
    public async Task<IActionResult> UpdateMinerals(int id,MineralsDto dto)
    {
        try
        {
            await _repo.UpdateMineralsAsync(id, dto);
            return NoContent();
        }
        catch (FoodItemException e)
        {
          return  BadRequest(e.Message);
        }
    }
    [HttpPatch("{id}/minerals")] //
    public async Task<IActionResult>PatchMacros( int id ,[FromBody]JsonPatchDocument<MacroNutrientsDto> patchDocument) 
    {
       MacroNutrientsDto dto = await _repo.GetMacrosAsync(id);
        if (dto is null) 
        {
            return NotFound();
        }
        patchDocument.ApplyTo(dto, ModelState);


        if (!ModelState.IsValid) 
        {
            return BadRequest(ModelState);
        }
       

        try
        {
            await UpdateMacros(id, dto);

            return NoContent();
        }
        catch(FoodItemException e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpPatch("{id}/macros")]
    public async Task<IActionResult>PatchMinerals(int id, [FromBody]JsonPatchDocument<MineralsDto> patchDocument) 
    {
        MineralsDto dto = await _repo.GetMineralsDtoAsync(id);
        if (dto is null) 
        {
            return NotFound();
        }
        patchDocument.ApplyTo(dto, ModelState);
        if (!ModelState.IsValid) 
        {
            return BadRequest(ModelState);
        }
        try
        {
            await UpdateMinerals(id, dto);
            return NoContent();
        }
        catch (FoodItemException e)
        {
            return BadRequest( e.Message);
        }
    }

    [HttpPut("{id}/macros")]
    public async Task<IActionResult> UpdateMacros(int id, MacroNutrientsDto dto)
    {
        try
        {
            await _repo.UpdateMacrosAsync(id, dto);
            return NoContent();
        }
        catch (FoodItemException e)
        {
          return  BadRequest(e.Message);
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
         FoodItemDto item =await _repo.GetByIdAsync(id);
        if (item is null)
        {
            return NotFound();
        }
            patchDocumet.ApplyTo(item,ModelState);
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

            return NotFound(e.Message);
        }
       
    }
       
    }
   

