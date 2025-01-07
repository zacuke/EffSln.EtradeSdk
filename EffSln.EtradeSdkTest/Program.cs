using EffSln.EtradeSdk.AuthorizationGetAccessToken;
using EffSln.EtradeSdk.AuthorizationRequestToken;
using System.Buffers.Text;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
using EffSln.EtradeSdk;
using Microsoft.Extensions.Configuration;

namespace EffSln.EtradeSdkTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            
            var configuration = new ConfigurationBuilder()
                 .AddUserSecrets<Program>() // Load secrets.json using UserSecrets
                 .Build();

            string key = configuration["key"];
            string secret = configuration["secret"];


            var httpMethod = "get";
            var url = "https://api.etrade.com/oauth/request_token";
            var parameters = new Dictionary<string, string>
            {
                { "oauth_consumer_key", key },
                { "oauth_nonce", "LTc4MTA4MTU5NjY4MDExNTUzMjc=" },
                { "oauth_timestamp", "1736210214" },
                { "oauth_signature_method", "HMAC-SHA1" },
                { "oauth_version", "1.0" },
                { "oauth_callback","oob" }


            };
            var signingKey = secret;
            var result = HMACSigning.GenerateSignature(httpMethod, url, parameters, signingKey);
            Console.WriteLine(result);
   
        }

        

    }
}
