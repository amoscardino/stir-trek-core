using Blazor.Extensions.Logging;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StirTrekCore.Services;
using System.Threading.Tasks;

namespace StirTrekCore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Register services
            builder.Services.AddLogging(builder => builder.AddBrowserConsole().SetMinimumLevel(LogLevel.Trace));
            builder.Services.AddStorage();
            builder.Services.AddSingleton<StirTrekService>();

            // Register root component
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
