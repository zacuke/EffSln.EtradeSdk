

using EffSln.EtradeSdk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EffSln.EtradeSdkTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateApplicationBuilder(args);
            hostBuilder.Services.AddEtradeSdk(options =>
              {
                  options.Key = hostBuilder.Configuration["key"];
                  options.Secret = hostBuilder.Configuration["secret"];
              });
            hostBuilder.Services.AddHostedService<Worker>();
            await hostBuilder.Build().RunAsync();

        }
    }
}
