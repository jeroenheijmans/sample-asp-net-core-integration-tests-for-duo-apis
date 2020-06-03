using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleDuoApis.BarApi
{
    [ApiController]
    [Route("beers")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class BeersController : ControllerBase
    {
        private readonly ILogger<BeersController> logger;

        private readonly IList<BeerDto> FakeBeerList = new List<BeerDto> 
        {
            new BeerDto { Id = "jb", Name = "Jeroenbrau" },
            new BeerDto { Id = "fw", Name = "Fink Weizen" },
            new BeerDto { Id = "te", Name = "Tripel d'Eimans" },
            new BeerDto { Id = "pn", Name = "Pils Normahl" },
            new BeerDto { Id = "sf", Name = "Stoute Flesch" },
            new BeerDto { Id = "bi", Name = "Beard IPA" },
        };

        public BeersController(ILogger<BeersController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<BeerDto[]> Get() => FakeBeerList.ToArray();

        [HttpGet("{id}")]
        public ActionResult<BeerDto> Get(string Id)
        {
            logger.LogInformation($"Returning beer with id {Id}");
            return FakeBeerList.SingleOrDefault(b => b.Id == Id);
        }
    }
}