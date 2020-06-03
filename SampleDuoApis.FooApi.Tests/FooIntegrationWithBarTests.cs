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
        public async Task FooRandomizer_WhenCalled_ReturnsItemFromBar()
        {
            var fooApiClient = fixture.CreateClient();
            var response = await fooApiClient.GetAsync("/randomizer");
            Assert.True(response.IsSuccessStatusCode);
            var data = await response.Content.ReadAsStringAsync();
            Assert.NotNull(data);
        }
    }
}