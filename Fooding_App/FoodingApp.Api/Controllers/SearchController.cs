using FoodingApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly FoodingAppRepoFacade _repos;

        public SearchController(FoodingAppRepoFacade repos)
        {
            _repos = repos;
        }

    }
}
