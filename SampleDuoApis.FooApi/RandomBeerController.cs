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
        public async Task<ActionResult<string>> Get()
        {
            this.logger.LogInformation("Being asked for a random beer");
            var beer = await this.bar.GetRandomBeer();
            this.logger.LogInformation("Randomly picked beer with id: " + beer.Id);
            return beer.Name;
        }
    }
}