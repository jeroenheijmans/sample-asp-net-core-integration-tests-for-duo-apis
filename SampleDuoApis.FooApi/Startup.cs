using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SampleDuoApis.FooApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Would really prefer to register with AddHttpClient, but that requires
            // changes to WebApplicationFactory for tests to overwrite the registration.
            services.AddTransient<BarService>();
            
            services.AddControllers();
            services.AddSingleton(new BarOptions { ApiBaseAddress = "http://localhost:5000" });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}
