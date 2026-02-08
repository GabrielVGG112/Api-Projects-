using FoodingApp.Api.CustomExceptions;
using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoodingApp.Api.Controllers
{
    [Route("api/fooditems/{id}/[controller]")]
    [ApiController]
    public class NutrientsController : ControllerBase
    {
        private readonly IFoodingItemRepository _repo;


        public NutrientsController(IFoodingItemRepository repo)

        {
            _repo = repo;


        }



        [HttpGet("vitamins")]
        public async Task<ActionResult<VitaminsDto>> GetVitamins(int id) =>await  ApplyGetAsync(id, _repo.GetVitaminsAsync);


        [HttpGet("macros")]
        public async Task<ActionResult<MacroNutrientsDto>> GetMacroNutrients(int id) => await ApplyGetAsync(id, _repo.GetMacrosAsync);

        [HttpGet("minerals")]
        public async Task<ActionResult<MineralsDto>> GetMinerals(int id) => await ApplyGetAsync(id ,_repo.GetMineralsAsync);


        [HttpPut("vitamins")]
        public async Task<IActionResult> UpdateVitamins(int id, VitaminsDto dto) =>
            await ApplyPutAsync(id, dto, _repo.UpdateVitaminsAsync);


        [HttpPut("minerals")] //
        public async Task<IActionResult> UpdateMinerals(int id, MineralsDto dto) =>
            await ApplyPutAsync(id, dto, _repo.UpdateMineralsAsync);




        [HttpPut("macros")] //
        public async Task<IActionResult> UpdateMacros(int id, MacroNutrientsDto dto) =>
            await ApplyPutAsync(id, dto, _repo.UpdateMacrosAsync);



        [HttpPatch("macros")] //
        public async Task<IActionResult> PatchMacros(int id, [FromBody] JsonPatchDocument<MacroNutrientsDto> patchDocument) =>
             await ApplyPatchAsync(id, patchDocument, _repo.GetMacrosAsync, _repo.UpdateMacrosAsync);



        [HttpPatch("minerals")] //
        public async Task<IActionResult> PatchMinerals(int id, [FromBody] JsonPatchDocument<MineralsDto> patchDocument) =>
            await ApplyPatchAsync(id, patchDocument, _repo.GetMineralsAsync, _repo.UpdateMineralsAsync);


        [HttpPatch("vitamins")]
        public async Task<IActionResult> PatchVitamins(int id, JsonPatchDocument<VitaminsDto> patchDocument) =>
             await ApplyPatchAsync(id, patchDocument, _repo.GetVitaminsAsync, _repo.UpdateVitaminsAsync);


        private async Task<ActionResult<T>> ApplyGetAsync<T>(int id, Func<int, Task<T>> getter) 
        {
            try
            {
                var vitamins = await getter(id);
                return Ok(vitamins);
            }
            catch (FoodItemException e)
            {
                return NotFound(e.Message);
            }
        }

        private async Task<IActionResult> ApplyPutAsync<TDto>(int id, TDto dto, Func<int, TDto, Task> updater)
        {
           
                await updater(id, dto);
                return NoContent();
    
        }


        public async Task<IActionResult> ApplyPatchAsync<TDto>(int id,
            JsonPatchDocument<TDto> patch,
            Func<int, Task<TDto>> getter, Func<int, TDto, Task> updater)
            where TDto : class, new()
        {
            var dto = await getter(id);
            if (dto is null)
            {
                return NotFound();
            }
            patch.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
                await updater(id, dto);
                return NoContent();
         
        }
    }
}
