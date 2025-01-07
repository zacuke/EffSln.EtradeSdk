
using EffSln.EtradeSdk.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
 

namespace EffSln.EtradeSdk;
public static class EtradeSdkExtensions
{
    private static IServiceProvider _serviceProvider;

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
        services.AddHttpClient<AuthorizationClient>()
                .AddHttpMessageHandler<OAuthDelegatingHandler>();

   

        // Assign the service provider for use in static methods
        var serviceProvider = services.BuildServiceProvider();
        _serviceProvider = serviceProvider;


        return services;
    }

    /// <summary>
    /// https://apisb.etrade.com/docs/api/authorization/authorize.html
    /// </summary>
    /// <param name="key"></param>
    /// <param name="Oauth_request_token"></param>
    /// <returns>Authorize Application Request URL</returns>
    public static string GetAuthorizeApplicationUrl(string Oauth_request_token)
    {
        var options = _serviceProvider.GetRequiredService<IOptions<EtradeSdkOptions>>().Value;
        return $"https://us.etrade.com/e/t/etws/authorize?key={options.Key}&token={Oauth_request_token}";

    }
}
