using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleDuoApis.FooApi.Tests
{
    public class DuoApiWebApplicationFactory : WebApplicationFactory<FooApi.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder fooApiBuilder)
        {
            base.ConfigureWebHost(fooApiBuilder);

            var barApiBuilder = new WebHostBuilder()
                .UseStartup<BarApi.Startup>();

            var BarApiServer = new TestServer(barApiBuilder);

            fooApiBuilder
                .ConfigureTestServices(services =>
                {
                    var barClient = BarApiServer.CreateClient();
                    services.AddSingleton<HttpClient>(barClient);
                    services.AddSingleton(new BarOptions { ApiBaseAddress = barClient.BaseAddress.ToString() });
                });
        }
    }
}