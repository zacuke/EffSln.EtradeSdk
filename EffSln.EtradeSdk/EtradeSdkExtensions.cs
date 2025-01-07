
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
    /// Once your application has the request token, it should redirect the user to an E*TRADE authorization page, as shown in the Authorize Application Request URL below. Note that this URL includes the request token and the consumer key as parameters. Running the URL opens up a page which asks the user to authorize the application. Once the user approves the authorization request, E*TRADE generates a verification code and displays it the Authorization Complete page. The user may then manually copy the code and paste it into the application. However, we recommend that the verification code be passed directly to the application via a preconfigured callback URL; in order to do this, the callback URL must be associated with the consumer key. Follow the instructions in the Authorization guide chapter to do this(https://developer.etrade.com/getting-started/developer-guides). The callback URL may be just a simple address or may also include query parameters. Once the callback is configured, users are automatically redirected to the specified URL with the verification code appended as a query parameter. Examples are shown in the Sample Response below.
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
