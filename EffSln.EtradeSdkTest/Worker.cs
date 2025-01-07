
using EffSln.EtradeSdk.Authorization.RequestToken;
 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace EffSln.EtradeSdkTest; 

public class Worker(IConfiguration configuration, RequestTokenClient requestTokenClient ) : IHostedService
{
 
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var result = await requestTokenClient.GetRequestTokenAsync();
        var _key = configuration["key"];
        var authUrl = $"https://us.etrade.com/e/t/etws/authorize?key={_key}&token={result.Oauth_token}";

        //etrade requires user interaction once a day
        Process.Start(new ProcessStartInfo(authUrl) );

        Console.WriteLine("Please enter the verification code from the browser:");
        var verificationcode = Console.ReadLine();
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
