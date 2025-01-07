
using EffSln.EtradeSdk.Authorization.RequestToken;
 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
 

namespace EffSln.EtradeSdkTest; 

public class Worker(RequestTokenClient requestTokenClient ) : IHostedService
{
 
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var result = await requestTokenClient.GetRequestTokenAsync();
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
