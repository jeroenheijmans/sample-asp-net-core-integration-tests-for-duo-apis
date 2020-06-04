using System;
using System.Threading.Tasks;
using Xunit;

namespace SampleDuoApis.FooApi.Tests
{
    public class FooIntegrationWithBarTests : IClassFixture<DuoApiWebApplicationFactory>
    {
        private DuoApiWebApplicationFactory fixture;

        public FooIntegrationWithBarTests(DuoApiWebApplicationFactory fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task FooRandomizerListBeers_WhenCalled_ReturnsListOfNames()
        {
            var fooApiClient = fixture.CreateClient();
            var response = await fooApiClient.GetAsync("/randomizer");
            Assert.True(response.IsSuccessStatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data); // Smoke test
            Assert.Contains("Jeroenbrau", data); // Smoke test
        }

        [Fact]
        public async Task FooRandomizerPickOne_WhenCalled_ReturnsItemFromBar()
        {
            var fooApiClient = fixture.CreateClient();
            var response = await fooApiClient.GetAsync("/randomizer/pick-one");
            Assert.True(response.IsSuccessStatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data); // Smoke test
        }
    }
}