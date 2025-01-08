using EffSln.EtradeSdk;
using EffSln.EtradeSdk.Accounts;
using EffSln.EtradeSdk.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace EffSln.EtradeSdkTest;

public class Worker(IConfiguration configuration, AuthorizationClient authorizationClient, AccountsClient accountsClient  ) : IHostedService
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
        var accessTokenResponse = await authorizationClient.GetAccessTokenAsync(requestToken.Oauth_token, requestToken.Oauth_token_secret, oauth_verifier, cancellationToken);
       
        var accounts = await accountsClient.ListAccountsAsync(accessTokenResponse.Oauth_token, accessTokenResponse.Oauth_token_secret, cancellationToken);
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
