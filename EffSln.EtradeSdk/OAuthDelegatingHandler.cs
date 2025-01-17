using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace EffSln.EtradeSdk;
public class OAuthDelegatingHandler : DelegatingHandler
{

    private readonly EtradeSdkOptions _etradeSdkOptions;
    public OAuthDelegatingHandler(EtradeSdkOptions options)
    {
        options.Validate();
        _etradeSdkOptions = options; 
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var oauth_token = string.Empty;
        var oauth_token_secret = string.Empty;
        var oauth_verifier = string.Empty;

        var _key = _etradeSdkOptions.Key;
        var _secret = _etradeSdkOptions.Secret;
        var _oauth_callback = _etradeSdkOptions.Oauth_callback;


        //adding this to prevent forcing user to pass the access tokens every time
        if (!string.IsNullOrEmpty(_etradeSdkOptions.AccessOauth_token))
        {
            oauth_token = _etradeSdkOptions.AccessOauth_token;
        }
        if (!string.IsNullOrEmpty(_etradeSdkOptions.AccessOauth_token_secret))
        {
            oauth_token_secret = _etradeSdkOptions.AccessOauth_token_secret;
        }

        //do this second so user can still manually call the auth methods
        //bit of a hack because nswag
        if (request.Headers.Contains("oauth_token"))
        {
            oauth_token = request.Headers.GetValues("oauth_token").First();
            request.Headers.Remove("oauth_token");
        }
        if (request.Headers.Contains("oauth_token_secret"))
        {
            oauth_token_secret = request.Headers.GetValues("oauth_token_secret").First();
            request.Headers.Remove("oauth_token_secret");
        }
        if (request.Headers.Contains("oauth_verifier"))
        {
            oauth_verifier = request.Headers.GetValues("oauth_verifier").First();
            request.Headers.Remove("oauth_verifier");
        }

        // 1. Generate OAuth parameters for the Authorization header
        var httpMethod = request.Method.Method.ToLower();
        var url = request.RequestUri.ToString();
        var timestamp = ComputeTimestamp();
        var nonce = ComputeNonce();
        var parameters = new Dictionary<string, string>
        {
            { "oauth_consumer_key", _key },
            { "oauth_nonce", nonce },
            { "oauth_timestamp", timestamp },
            { "oauth_signature_method", "HMAC-SHA1" },

        };

        if (oauth_token != string.Empty)
        {
            parameters.Add("oauth_token", oauth_token);
            if (oauth_verifier != string.Empty)
            {
                parameters.Add("oauth_verifier", oauth_verifier);
            }
        }
        else
        {
            parameters.Add("oauth_callback", _oauth_callback);
            //parameters.Add("oauth_version", "1.0");
        }

        GetQueryStringMap(url, ref parameters);
        Uri uri = new Uri(url);
 
        var signature = GenerateSignature(httpMethod, $"{uri.Scheme}://{uri.Host}{uri.AbsolutePath}", parameters, _secret, oauth_token_secret);

        // 2. Build the dynamically signed Authorization header
        parameters.Add("oauth_signature", signature);

        var authorizationHeader = "OAuth " + string.Join(",", parameters
             .Select(kvp => $"{PercentEncode(kvp.Key)}=\"{PercentEncode(kvp.Value)}\""));
    
        // 3. Add the Authorization header to the request
        request.Headers.Add("Authorization", authorizationHeader);

        // 4. Proceed with the request
        return await base.SendAsync(request, cancellationToken);
    }

    private readonly static Random secureRand = new  ();


    private static string ComputeNonce()
    {
        // Generate a long value similar to Java's nextLong
        long generatedNo = (long)(secureRand.NextDouble() * long.MaxValue);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(generatedNo.ToString()));
    }

    private static string ComputeTimestamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
    }

    private static string GenerateSignature(string httpMethod, string Url, Dictionary<string, string> keyValuePairs, string secretKey, string tokenSecret = "")
    {
        // Step 1: Sort key-value pairs lexically (ascending order by key, then by value if keys are identical)
        var sortedParameters = keyValuePairs
            .OrderBy(kvp => kvp.Key)
            .ThenBy(kvp => kvp.Value)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        // Step 2: Percent-encode the keys and values according to the OAuth spec
        var encodedKeyValuePairs = sortedParameters
            .Select(kvp => $"{PercentEncode(kvp.Key)}={PercentEncode(kvp.Value)}");

        // Step 3: Build the parameter string by joining encoded key-value pairs with '&'
        var parameterString = string.Join("&", encodedKeyValuePairs);

        // Step 4: Percent-encode the URL
        var encodedUrl = PercentEncode(Url);

        // Step 5: Construct the base string
        var baseString = $"{httpMethod.ToUpper()}&{encodedUrl}&{PercentEncode(parameterString)}";

        // Step 6: Call GenerateSignature2 to generate the signature using the HMAC-SHA1 mechanism
        return HMACEncode(baseString, secretKey, tokenSecret);
    }
    private static string HMACEncode(string baseString, string secretKey, string tokenSecret = "")
    {
        // Ensure signingKey ends with '&' (as per OAuth spec, only provided consumer_secret&token_secret format works)
        var finalSecret = secretKey + "&" + PercentEncode(tokenSecret);

        // Compute the HMAC-SHA1 hash using the signingKey and the baseString
        using var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(finalSecret));
        byte[] hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(baseString));
        return Convert.ToBase64String(hash); // Convert hash to Base64 string
    }

    private static string PercentEncode(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        // Use StringBuilder to correctly percent-encode per RFC 3986
        var sb = new StringBuilder();
        foreach (char c in value)
        {
            if ((c >= 'A' && c <= 'Z') ||        // A-Z
                (c >= 'a' && c <= 'z') ||        // a-z
                (c >= '0' && c <= '9') ||        // 0-9
                c == '-' || c == '_' || c == '.' || c == '~') // Unreserved characters
            {
                sb.Append(c);
            }
            else
            {
                // Convert non-unreserved characters into upper-case percent-encoding
                sb.Append('%' + $"{(int)c:X2}");
            }
        }
        return sb.ToString();
    }

    private void GetQueryStringMap(string url, ref Dictionary<string, string> queryParamMap)
    {
        // Ensure the dictionary is initialized
        if (queryParamMap == null)
        {
            queryParamMap = new Dictionary<string, string>();
        }

        // Extract the query string from the URL
        var uri = new Uri(url);
        string queryString = uri.Query;

        if (!string.IsNullOrEmpty(queryString))
        {
            // Remove the leading '?' from the query string
            queryString = queryString.TrimStart('?');

            // Split the query string by '&'
            foreach (string keyValue in queryString.Split('&'))
            {
                // Split on '=' to separate key and value
                string[] p = keyValue.Split('=');

                // Ensure that we handle potential issues where there might not be a key-value pair
                if (p.Length == 2)
                {
                    queryParamMap[p[0]] =  p[1] ;
                }
                else if (p.Length == 1) // Handle cases where there's a key with no value
                {
                    queryParamMap[p[0]] =  string.Empty ;
                }
            }
        }
    }
}