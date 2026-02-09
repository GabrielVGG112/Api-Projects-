using FoodingApp.Api.Dtos;
using FoodingApp.Api.Services.Interfaces;
using FoodingApp.Library.Dtos;
using FoodingApp.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRepository _repo;

        public SearchController(ISearchRepository repo)
        {
            _repo = repo;
        }
        [HttpPost]
        public async Task<ActionResult<PagedResultDto<FoodItemDto>>> GetFoodItemByQuery([FromBody] FoodSearchQueryDto searchQuery,CancellationToken ct) 
        {
           return Ok(await _repo.GetFoodItemAsync(searchQuery, ct));
        }

    }
}
