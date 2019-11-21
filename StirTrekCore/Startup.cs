using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using StirTrekCore.Services;
using Blazor.Extensions.Storage;
using Blazor.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace StirTrekCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddBrowserConsole().SetMinimumLevel(LogLevel.Trace));

            services.AddStorage();
            services.AddSingleton<StirTrekService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
