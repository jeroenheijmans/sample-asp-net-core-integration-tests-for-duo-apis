using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SampleDuoApis.FooApi
{
    public class BarService
    {
        private static readonly string[] beerIdShortList = new[] { "fw", "jb", "te", "pn", "sf", "bi" };
        private readonly ILogger<BarService> logger;
        private readonly HttpClient httpClient;

        public BarService(ILogger<BarService> logger, HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
        }

        public async Task<BeerDto> GetRandomBeer()
        {
            var length = beerIdShortList.Length;
            var id = beerIdShortList[DateTime.UtcNow.Ticks % length];
            logger.LogInformation($"Randomly picked id to retrieve beef for: {id}");
            return await httpClient.Get<BeerDto>($"/beers/{id}");
        }
    }
}