using EffSln.EtradeSdk.Authorization.GetRequestToken;
using EffSln.EtradeSdk.Authorization.GetAccessToken;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

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
        services.AddHttpClient<GetRequestTokenClient>()
                .AddHttpMessageHandler<OAuthDelegatingHandler>();

        services.AddHttpClient<GetAccessTokenClient>()
                .AddHttpMessageHandler<OAuthDelegatingHandler>();
        return services;
    }
}
