using EffSln.EtradeSdk;
using EffSln.EtradeSdk.Accounts;
using EffSln.EtradeSdk.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading;

namespace EffSln.EtradeSdkTest;

public class Worker : IHostedService
{
    private readonly EtradeSdkOptions _etradeSdkOptions;
    private readonly AuthorizationClient _authorizationClient;
    private readonly AccountsClient _accountsClient;    
    public Worker(AuthorizationClient authorizationClient, AccountsClient accountsClient, IOptions<EtradeSdkOptions> etradeSdkOptions)
    {
        _authorizationClient = authorizationClient;
        _accountsClient = accountsClient;
        _etradeSdkOptions = etradeSdkOptions.Value;
    }

 
    public async Task StartAsync(CancellationToken cancellationToken)
    {        
 
        //first get access token
        var requestToken = await _authorizationClient.GetRequestTokenAsync(cancellationToken);
        var authUrl = EtradeSdkExtensions.GetAuthorizeApplicationUrl(requestToken.Oauth_token);
        var accessTokenResponse = await GetTokenFromUser(authUrl, requestToken.Oauth_token, requestToken.Oauth_token_secret, cancellationToken);

        //based on etrade docs these will expire at end of day
        _etradeSdkOptions.AccessOauth_token = accessTokenResponse.Oauth_token;
        _etradeSdkOptions.AccessOauth_token_secret = accessTokenResponse.Oauth_token_secret;

        //now we can start calling the normal API methods
        var accounts = await _accountsClient.ListAccountsAsync(cancellationToken);
    }

    /// <summary>
    /// Example method to open browser where user logs into etrade and gets a token. Etrade has an optional Callback URL that can be implemented instead to avoid copy pasting the token, but it still requires user interaction to sign into Etrade and click Agree.
    /// </summary>
    /// <param name="authUrl"></param>
    /// <param name="oauth_token"></param>
    /// <param name="oauth_token_secret"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<OAuthGetAccessTokenResponse> GetTokenFromUser(string authUrl,string oauth_token, string oauth_token_secret, CancellationToken cancellationToken)
    {
        //etrade requires user interaction once a day
        var launchBrowser = new ProcessStartInfo(authUrl) { UseShellExecute = true };
        Process.Start(launchBrowser);

        Console.Write("Please enter the verification code from the browser: ");
        var oauth_verifier = Console.ReadLine();
        var accessTokenResponse = await _authorizationClient.GetAccessTokenAsync(oauth_token, oauth_token_secret, oauth_verifier, cancellationToken);
        return accessTokenResponse;

    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
