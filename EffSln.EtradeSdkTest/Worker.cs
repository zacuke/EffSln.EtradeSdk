
using EffSln.EtradeSdk.Authorization.GetAccessToken;
using EffSln.EtradeSdk.Authorization.GetRequestToken;
 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace EffSln.EtradeSdkTest; 

public class Worker(IConfiguration configuration, GetRequestTokenClient requestTokenClient, GetAccessTokenClient getAccessTokenClient ) : IHostedService
{
 
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var requestToken = await requestTokenClient.GetRequestTokenAsync();
        var _key = configuration["key"];
        var authUrl = $"https://us.etrade.com/e/t/etws/authorize?key={_key}&token={requestToken.Oauth_token}";

        //etrade requires user interaction once a day
        var launchBrowser = new ProcessStartInfo(authUrl) { UseShellExecute = true}; 
        Process.Start(launchBrowser);

        Console.WriteLine("Please enter the verification code from the browser:");
        var Oauth_verifier = Console.ReadLine();
        var accessToken = await getAccessTokenClient.GetAccessTokenAsync(requestToken.Oauth_token, requestToken.Oauth_token_secret, Oauth_verifier);

    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
