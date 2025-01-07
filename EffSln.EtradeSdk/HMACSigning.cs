using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EffSln.EtradeSdk
{
    public static class HMACSigning
    {
        public static string GenerateSignature(string httpMethod, string Url, Dictionary<string, string> keyValuePairs, string secretKey, string tokenSecret="")
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
            var finalSecret = secretKey + "&" + tokenSecret;

            // Compute the HMAC-SHA1 hash using the signingKey and the baseString
            using (var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(finalSecret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(baseString));
                return Convert.ToBase64String(hash); // Convert hash to Base64 string
            }
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
    }
}
