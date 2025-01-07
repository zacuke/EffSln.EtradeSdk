//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace EffSln.EtradeSdk.Authorization.RequestToken;

//public partial class RequestTokenClient  
//{
//    protected override async ValueTask  ProcessResponseAsync(HttpClient client,  HttpResponseMessage response, CancellationToken cancellationToken)
//    {
//        if (response.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded")
//        {
//            // Read as string
//            var content = await response.Content.ReadAsStringAsync();

//            // Parse it manually
//            var parsedResult = content.Split('&')
//                .Select(param => param.Split('='))
//                .ToDictionary(
//                    pair => Uri.UnescapeDataString(pair[0]),
//                    pair => Uri.UnescapeDataString(pair[1])
//                ); 

//            // Map to your response object if needed (e.g., OAuthTokenResponse)
//            var oauthResponse = new Response
//            {
//                Oauth_token = parsedResult["oauth_token"],
//                Oauth_token_secret = parsedResult["oauth_token_secret"],
//                Oauth_callback_confirmed = parsedResult["oauth_callback_confirmed"]
//            };

//        }

//    }
//}
