using EffSln.EtradeSdk;
using EffSln.EtradeSdk.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace EffSln.EtradeSdkTest;

public class Worker(IConfiguration configuration, AuthorizationClient authorizationClient  ) : IHostedService
{
 
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var requestToken = await authorizationClient.GetRequestTokenAsync(cancellationToken);

        var authUrl = EtradeSdkExtensions.GetAuthorizeApplicationUrl(requestToken.Oauth_token);

        //etrade requires user interaction once a day
        var launchBrowser = new ProcessStartInfo(authUrl) { UseShellExecute = true}; 
        Process.Start(launchBrowser);

        Console.Write("Please enter the verification code from the browser:");
        var oauth_verifier = Console.ReadLine();
        var accessToken = await authorizationClient.GetAccessTokenAsync(requestToken.Oauth_token, requestToken.Oauth_token_secret, oauth_verifier, cancellationToken);

       // var response = await revokeAccessTokenClient.RevokeAccessTokenAsync(accessToken.Oauth_token);
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
