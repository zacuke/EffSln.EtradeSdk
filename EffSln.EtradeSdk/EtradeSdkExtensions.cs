using EffSln.EtradeSdk.Authorization.RequestToken;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffSln.EtradeSdk;
public static class EtradeSdkExtensions
{
    public static IServiceCollection AddEtradeSdk(this IServiceCollection services, Action<EtradeSdkOptions> configureOptions)
    {
        // Register SdkOptions (single, global configuration)
        services.Configure(configureOptions);

        // Register the OAuthDelegatingHandler
        services.AddTransient(provider =>
        {
            var options = provider.GetRequiredService<IOptions<EtradeSdkOptions>>().Value;
            return new OAuthDelegatingHandler(options);
        });

        // Register the HttpClient and bind it to RequestTokenClient
        services.AddHttpClient<RequestTokenClient>()
                .AddHttpMessageHandler<OAuthDelegatingHandler>();

        return services;
    }
}
