using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleDuoApis.FooApi
{
    [ApiController]
    [Route("randomizer")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class RandomBeerController : ControllerBase
    {
        private readonly ILogger<RandomBeerController> logger;
        private readonly BarService bar;

        public RandomBeerController(ILogger<RandomBeerController> logger, BarService bar)
        {
            this.logger = logger;
            this.bar = bar;
        }

        [HttpGet]
        public async Task<ActionResult<string[]>> Get()
        {
            this.logger.LogInformation("Being asked for a randomized list of beers");
            var beers = await this.bar.GetAllBeersInRandomOrder();
            var beerNames = beers.Select(b => b.Name).ToArray();
            this.logger.LogInformation("Randomly list retrieved was: " + string.Join(", ", beers.Select(b => b.Name).ToArray()));
            return beerNames;
        }

        [HttpGet("pick-one")]
        public async Task<ActionResult<string>> GetOne()
        {
            this.logger.LogInformation("Being asked for a random beer");
            var beer = await this.bar.GetRandomBeer();
            this.logger.LogInformation("Randomly picked beer with id: " + beer.Id);
            return beer.Name;
        }
    }
}