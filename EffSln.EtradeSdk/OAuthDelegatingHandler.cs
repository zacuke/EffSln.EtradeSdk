using EffSln.EtradeSdk;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;

public class OAuthDelegatingHandler : DelegatingHandler
{
    private readonly string _key;
    private readonly string _secret;

    public OAuthDelegatingHandler(IConfiguration configuration)
    {
        // Load your 'key' and 'secret' (or inject with DI)
        _key = configuration["key"];
        _secret = configuration["secret"];
        if (string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_secret))
        {
            throw new Exception("Key and secret must be provided.");
        }
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // 1. Generate OAuth parameters for the Authorization header
        var httpMethod = request.Method.Method.ToLower();
        var url = request.RequestUri.ToString();
        var timestamp = OAuthHelper.ComputeTimestamp();
        var nonce = OAuthHelper.ComputeNonce();
        var parameters = new Dictionary<string, string>
        {
            { "oauth_consumer_key", _key },
            { "oauth_nonce", nonce },
            { "oauth_timestamp", timestamp },
            { "oauth_signature_method", "HMAC-SHA1" },
            { "oauth_version", "1.0" },
            { "oauth_callback", "oob" }
        };

        var signature = HMACSigning.GenerateSignature(httpMethod, url, parameters, _secret);

        // 2. Build the dynamically signed Authorization header
        var authorizationHeader = $"OAuth oauth_consumer_key=\"{_key}\",oauth_timestamp=\"{timestamp}\",oauth_nonce=\"{HMACSigning.PercentEncode(nonce)}\",oauth_signature_method=\"HMAC-SHA1\",oauth_signature=\"{HMACSigning.PercentEncode(signature)}\",oauth_callback=\"oob\",oauth_version=\"1.0\"";

        // 3. Add the Authorization header to the request
        request.Headers.Add("Authorization", authorizationHeader);

        // 4. Proceed with the request
        return await base.SendAsync(request, cancellationToken);
    }
}