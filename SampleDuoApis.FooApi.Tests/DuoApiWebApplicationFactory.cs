using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                    // In the normal Startup, the BarService is registered as a 
                    // "typed" HttpClient, using AddHttpClient. This effectively
                    // registers the service as "Transient" with a special factory
                    // for the HttpClient (that reuses pooled Message Handlers for
                    // improved sockets pooling). We can't easily "overwrite" the
                    // provided HttpClient for that service, so we just completely
                    // manually construct the BarService for tests:
                    var barClient = BarApiServer.CreateClient();
                    services.AddTransient<BarService>(provider => new BarService(
                        provider.GetRequiredService<ILogger<BarService>>(),
                        barClient
                    ));
                });
        }
    }
}