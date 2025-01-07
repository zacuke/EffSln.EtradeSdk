 
using System.Buffers.Text;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;
 
using EffSln.EtradeSdk;
using Microsoft.Extensions.Configuration;
using EffSln.EtradeSdk.Authorization.RequestToken;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EffSln.EtradeSdkTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateApplicationBuilder(args) ;
           
            hostBuilder.Services.AddHttpClient<RequestTokenClient>()
                .AddHttpMessageHandler<OAuthDelegatingHandler>();
            hostBuilder.Services.AddTransient<OAuthDelegatingHandler>();
    
            hostBuilder.Services.AddHostedService<Worker>();
            await hostBuilder.Build().RunAsync();

            //var configuration = new ConfigurationBuilder()
            //     .AddUserSecrets<Program>() // Load secrets.json using UserSecrets
            //     .Build();

            //string key = configuration["key"];
            //string secret = configuration["secret"];


            //var httpMethod = "get";
            //var url = "https://api.etrade.com/oauth/request_token";
            //var timestamp = OAuthHelper.ComputeTimestamp();
            //var nonce = OAuthHelper.ComputeNonce();
            //var parameters = new Dictionary<string, string>
            //{
            //    { "oauth_consumer_key", key },
            //    { "oauth_nonce", nonce },
            //    { "oauth_timestamp", timestamp },
            //    { "oauth_signature_method", "HMAC-SHA1" },
            //    { "oauth_version", "1.0" },
            //    { "oauth_callback","oob" }


            //};
            //var signingKey = secret;
            //var signature = HMACSigning.GenerateSignature(httpMethod, url, parameters, signingKey);
            ////Console.WriteLine(result);
            //var httpClient = new HttpClient();
            //var client = new RequestTokenClient(httpClient);
            //var authorization = $"OAuth oauth_consumer_key=\"{key}\",oauth_timestamp=\"{timestamp}\",oauth_nonce=\"{HMACSigning.PercentEncode(nonce)}\",oauth_signature_method=\"HMAC-SHA1\",oauth_signature=\"{HMACSigning.PercentEncode(signature)}\",oauth_callback=\"oob\",oauth_version=\"1.0\"";
            //var result = await client.GetRequestTokenAsync(authorization);

        }



    }
}
